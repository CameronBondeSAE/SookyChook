using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Aaron
{
    public class Pathfinding : MonoBehaviour
    {
        public Vector2Int beginning, finish;

        private Vector3 beginningPos;

        private int distanceX;
        private int distanceY;
        
        public List<ScanningGrid.Node> openSet = new List<ScanningGrid.Node>();
        public List<ScanningGrid.Node> closedSet = new List<ScanningGrid.Node>();

        public List<ScanningGrid.Node> path = new List<ScanningGrid.Node>();

        private ScanningGrid grid;

        private void Start()
        {
            grid = GetComponent<ScanningGrid>();
            FindPath(beginning, finish);
        }

        private void Update()
        {
            //TODO get coords from node world position; use to create paths on the go
        }

        //get start and finish points in Node Space
        void FindPath(Vector2Int start, Vector2Int end)
        {
            ScanningGrid.Node endNode = grid.grid[end.x, end.y];
            ScanningGrid.Node currentNode = grid.grid[start.x, start.y];

            openSet.Add(currentNode);
            currentNode.coords = new Vector2Int(start.x, start.y);
            currentNode.gCost = 0;
            currentNode.hCost = GetDistance(currentNode.coords, end);
            
            while (currentNode != grid.grid[end.x, end.y])
            {
                int currentLowestFCost = currentNode.fCost;
                foreach (var node in openSet)
                {
                    if (node.fCost <= currentLowestFCost)
                    {
                        {
                            currentLowestFCost = node.fCost;
                            currentNode = node;
                        }
                    }
                }
                
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == grid.grid[end.x, end.y]) 
                {
                    break;
                }
                
                //checking neighbours surrounding currentNode; within grid
                for (int x = (currentNode.coords.x - 1); x < (currentNode.coords.x + 2); x++)
                {
                    for (int y = (currentNode.coords.y - 1); y < (currentNode.coords.y + 2); y++)
                    {
                        if (x > 0 && x <= grid.gridSizeX && y > 0 && y <= grid.gridSizeY)
                        {
                            //neighbour location
                            ScanningGrid.Node neighbour = grid.grid[x,y];
                            neighbour.coords = new Vector2Int(x, y);

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

                path.Clear();
                
                //in place of the RetracePath() function
                while (currentNode != grid.grid[beginning.x, beginning.y])
                {
                    path.Add(currentNode);
                    currentNode = currentNode.parent;
                }
            }
        }


        public int GetDistance(Vector2Int start, Vector2Int end)
        {
            Vector2Int distance = end - start;

            distance = new Vector2Int(Mathf.Abs(distance.x), Mathf.Abs(distance.y));

            if (distance.x > distance.y)
            {
                return distance.y * 14 + 10 * (distance.x - distance.y);
            }

            return distance.x * 14 + 10 * (distance.y-distance.x);
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
                for (int x = 0; x < grid.gridSizeX; x++)
                {
                    for (int y = 0; y < grid.gridSizeY; y++)
                    {
                        if (openSet.Contains(grid.grid[x, y]))
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawCube(new Vector3(x, y, 0), Vector3.one * (1 - 0.1f));
                        }

                        if (closedSet.Contains(grid.grid[x, y]))
                        {
                            Gizmos.color = Color.red;
                            Gizmos.DrawCube(new Vector3(x, y, 0), Vector3.one * (01 - .01f));
                        }

                        /*if (path.Contains(grid.grid[x, y]))
                        {
                            Gizmos.color = Color.green;
                            Gizmos.DrawCube(new Vector3(x, y, 0), Vector3.one * (01 - .01f));
                        }*/

                        Gizmos.color = Color.white;
                        Gizmos.DrawCube(new Vector3(beginning.x, beginning.y, 0), Vector3.one * (01 - .01f));

                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(new Vector3(finish.x, finish.y, 0), Vector3.one * (1 - 0.1f));
                    }
                }
        }
    }
}