using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rob
{
    public class PathFinding : MonoBehaviour
    {
        public Vector3Int endPos;
        //public Vector3Int startPos;

        public bool autoStart;

        [SerializeField] private WorldScan grid;

        public List<WorldScan.Node> openNodes = new List<WorldScan.Node>();
        public List<WorldScan.Node> closedNodes = new List<WorldScan.Node>();
        List<WorldScan.Node> path = new List<WorldScan.Node>();

        private int endCost;
        //private int lowestFCost;


        [SerializeField] private Vector3Int startPos;
        

        private WorldScan.Node endNode;
        WorldScan.Node startNode;
        WorldScan.Node currentNode;
        WorldScan.Node neighbour;

        [SerializeField] private bool debug;
        
        private void Awake()
        {
            // grid = FindObjectOfType<WorldScan>();
        }

        private void Start()
        {
            //currentPos = startPos;
            endNode = grid.gridNodeReference[endPos.x, endPos.z];
            openNodes.Clear();
            closedNodes.Clear();
            if (autoStart)
            {
                //StartCoroutine(FindPath());
                FindPath();
            }
        }

        //IEnumerator FindPath()
        public void FindPath()
        {
            startNode = grid.gridNodeReference[startPos.x, startPos.z];
            openNodes.Add(startNode);

            //int lowestFCost = Int32.MaxValue;
            //currentNode.fCost = Int32.MaxValue;
            //currentNode.hCost = Int32.MaxValue;
            //currentNode.gCost = Int32.MaxValue;

            while (openNodes.Count > 0)
            {
                currentNode = openNodes[0];
                for (int i = 1; i < openNodes.Count; i++)
                {
                    //lowestFCost = Int32.MaxValue;
                    if (openNodes[i].fCost < currentNode.fCost || openNodes[i].fCost == currentNode.fCost &&
                        openNodes[i].hCost < currentNode.hCost)
                    {
                        //lowestFCost = openNodes[i].fCost;
                        currentNode = openNodes[i];
                    }
                }

                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);


                if (currentNode == endNode)
                {
                    Debug.Log("Reached End");
                    RetracePath(startNode, endNode);
                    break;
                }


                // Neighbours


                for (int neighbourX = currentNode.gridPos.x - 1;
                    neighbourX < currentNode.gridPos.x + 2;
                    neighbourX++)
                {
                    for (int neighbourZ = currentNode.gridPos.z - 1;
                        neighbourZ < currentNode.gridPos.z + 2;
                        neighbourZ++)
                    {
                        // Check edges
                        if (neighbourX >= 0 && neighbourX < grid.maxSizeofGrid.x && neighbourZ >= 0 &&
                            neighbourZ < grid.maxSizeofGrid.z)
                        {
                            neighbour = grid.gridNodeReference[neighbourX, neighbourZ];


                            if (neighbour == currentNode)
                            {
                                continue;
                            }

                            if (neighbour.isBlocked || closedNodes.Contains(neighbour))
                            {
                                continue;
                            }


                            int neDistance = (int)(10 * Vector3.Distance(currentNode.gridPos, neighbour.gridPos));
                            int gCost = currentNode.gCost + neDistance;
                            

                            if (gCost < neighbour.gCost || !openNodes.Contains(neighbour))
                            {
                                neighbour.gCost = gCost;
                                neighbour.hCost = (int)(10 * Vector3.Distance(neighbour.gridPos, endPos));
                                neighbour.parent = currentNode;
                            }

                            if (!openNodes.Contains(neighbour))
                            {
                                openNodes.Add(neighbour);
                            }
                        }
                    }


                    // Remove current from open. Add current to closed
                    // Open nodes foreach looper
                    //      Find closest to endpos
                    //      Make that the new CurrentNode
                }
                
                //yield return new WaitForSeconds(.2f);
            }
        }

        void RetracePath(WorldScan.Node start, WorldScan.Node end)
        {
            currentNode = end;

            while (currentNode != start)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
        }
        


        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(startPos, Vector3.one);

            Gizmos.color = Color.grey;
            Gizmos.DrawCube(endPos, Vector3.one);

            foreach (WorldScan.Node openNode in openNodes)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(openNode.gridPos, Vector3.one);
            }


            foreach (WorldScan.Node closedNode in closedNodes)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(closedNode.gridPos, Vector3.one);
            }

            foreach (WorldScan.Node nodePath in path)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(nodePath.gridPos, Vector3.one);
            }

            Gizmos.color = Color.green;
            if (currentNode != null) Gizmos.DrawCube(currentNode.gridPos, Vector3.one);

            Gizmos.color = Color.magenta;
            if (neighbour != null) Gizmos.DrawCube(neighbour.gridPos, Vector3.one);
        }
    }
}