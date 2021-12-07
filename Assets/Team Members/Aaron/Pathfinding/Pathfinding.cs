using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;

namespace Aaron
{
    public class Pathfinding : MonoBehaviour
    {
        public Vector3Int beginning, finish;

        private Vector3 beginningPos;

        public Transform Beginning;
        public Transform Finish;

        private int BeginningX;
        private int BeginningY;
        private int BeginningZ;
        
        private int FinishX;
        private int FinishY;
        private int FinishZ;

        private int distanceX;
        private int distanceZ;
        
        public List<ScanningGrid.Node> openSet = new List<ScanningGrid.Node>();
        public List<ScanningGrid.Node> closedSet = new List<ScanningGrid.Node>();

        public List<ScanningGrid.Node> path = new List<ScanningGrid.Node>();

        private ScanningGrid grid;
        private ScanningGrid.Node currentNode;

        private void Start()
        {
            grid = ScanningGrid.Instance;

            VectorToInt();

            FindPath(beginning, finish);        }

        private void FixedUpdate()
        {
            //TODO get coords from node world position; use to create paths on the go
        }

        //get start and finish points in Node Space
        public void FindPath(Vector3Int start, Vector3Int end)
        {
            path.Clear();
            openSet.Clear();
            closedSet.Clear();
            VectorToInt();
            ScanningGrid.Node endNode = grid.grid[end.x, end.z];
            currentNode = grid.grid[start.x, start.z];

            openSet.Add(currentNode);
            currentNode.coords = new Vector3Int(start.x, 0, start.z);
            currentNode.gCost = 0;
            currentNode.hCost = GetDistance(currentNode.coords, end);
            
            while (currentNode != grid.grid[end.x, end.z])
            {
                var findLowestFCost = FindLowestFCost();
                if (findLowestFCost != null) currentNode = findLowestFCost;

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == grid.grid[end.x, end.z]) 
                {
                    break;
                }
                
                //checking neighbours surrounding currentNode; within grid
                for (int x = (currentNode.coords.x - 1); x < (currentNode.coords.x + 2); x++)
                {
                    for (int z = (currentNode.coords.z - 1); z < (currentNode.coords.z + 2); z++)
                    {
                        if (x > 0 && x <= grid.gridSizeX && z > 0 && z <= grid.gridSizeZ)
                        {
                            //neighbour location
                            ScanningGrid.Node neighbour = grid.grid[x,z];
                            neighbour.coords = new Vector3Int(x, 0, z);

                            if (neighbour.isBlocked || closedSet.Contains(neighbour))
                            {
                                continue;
                            }

                            int gCostToNextNeighbour =
                                (currentNode.gCost + GetDistance(currentNode.coords, neighbour.coords));

                            if (gCostToNextNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                            {
                                neighbour.gCost = gCostToNextNeighbour;
                                neighbour.hCost = GetDistance(neighbour.coords, end);

                                neighbour.parent = currentNode;
                                
                                

                                if (!openSet.Contains(neighbour))
                                {
                                    openSet.Add(neighbour);
                                }
                            }
                        }
                    }
                }
            }
            
            path.Clear();
            
            //in place of the RetracePath() function
            while (currentNode != grid.grid[beginning.x, beginning.z])
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
        }

        private ScanningGrid.Node FindLowestFCost()
        {
            int currentLowestFCost = Int32.MaxValue;
            ScanningGrid.Node lowestNode = null;
            
            foreach (var node in openSet)
            {
                if (node.fCost < currentLowestFCost)
                {
                    
                        currentLowestFCost = node.fCost;
                        lowestNode = node;

                }
            }

            return lowestNode;
        }

        public int GetDistance(Vector3Int start, Vector3Int end)
        {
            Vector3Int distance = end - start;

            distance = new Vector3Int(Mathf.Abs(distance.x), Mathf.Abs(distance.y), Mathf.Abs(distance.z));

            if (distance.x > distance.z)
            {
                return distance.z * 14 + 10 * (distance.x - distance.z);
            }

            return distance.x * 14 + 10 * (distance.z-distance.x);
        }

        private void VectorToInt()
        {
            BeginningX = Mathf.RoundToInt(Beginning.position.x);
            BeginningY = Mathf.RoundToInt(Beginning.position.y);
            BeginningZ = Mathf.RoundToInt(Beginning.position.z);

            FinishX = Mathf.RoundToInt(Finish.position.x);
            FinishY = Mathf.RoundToInt(Finish.position.y);
            FinishZ = Mathf.RoundToInt(Finish.position.z);
            
            beginning = new Vector3Int(BeginningX, BeginningY, BeginningZ);
            finish = new Vector3Int(FinishX, FinishY, FinishZ);
        }

        //possibly reintroduce if using coords from world space?
        /*void RetracePath(ScanningGrid.Node startNode, ScanningGrid.Node endNode)
        {
            ScanningGrid.Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
        }*/

        private void OnDrawGizmos()
        {
            if (grid != null)
            {

                /*foreach (var node in openSet)
                {
                    Gizmos.color = new Color(1, 0.92f, .16f, 0.5f);
                    Gizmos.DrawCube(new Vector3(node.coords.x, node.coords.y, node.coords.z), Vector3.one * (1 - 0.1f));
                    //Handles.Label(new Vector3(node.coords.x-.5f, (node.coords.y), node.coords.z), $"f={node.fCost}, h={node.hCost}, g={node.gCost}");

                }

                foreach (var node in closedSet)
                {
                    Gizmos.color = new Color(1, 0, 0, .5f);
                    Gizmos.DrawCube(new Vector3(node.coords.x, node.coords.y, node.coords.z), Vector3.one * (01 - .01f));
                    //Handles.Label(new Vector3(node.coords.x-.5f, (node.coords.y), node.coords.z), $"f={node.fCost}, h={node.hCost}, g={node.gCost}");

                }*/

                foreach (var node in path)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(new Vector3(node.coords.x, node.coords.y, node.coords.z), Vector3.one * (01 - .01f));
                }
                
                Gizmos.color = Color.white;
                Gizmos.DrawCube(new Vector3(beginning.x, beginning.y, beginning.z), Vector3.one * (01 - .01f));

                Gizmos.color = Color.black;
                Gizmos.DrawCube(new Vector3(finish.x, finish.y, finish.z), Vector3.one * (1 - 0.1f));
            }
            if (currentNode != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(new Vector3(currentNode.coords.x, currentNode.coords.y, currentNode.coords.z), Vector3.one);
            }
        }
    }
}