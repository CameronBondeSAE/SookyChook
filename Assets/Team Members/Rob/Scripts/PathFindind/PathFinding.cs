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

        private List<WorldScan.Node> openNodes = new List<WorldScan.Node>();
        private List<WorldScan.Node> closedNodes = new List<WorldScan.Node>();

        private int endCost;
        //private int lowestFCost;


        [SerializeField] private Vector3Int startPos;

        private void Awake()
        {
            // grid = FindObjectOfType<WorldScan>();
        }

        private void Start()
        {
            //currentPos = startPos;
            openNodes.Clear();
            closedNodes.Clear();
            if (autoStart)
            {
                StartCoroutine(FindPath());
            }
        }

        IEnumerator FindPath()
        {
            WorldScan.Node currentNode = grid.gridNodeReference[startPos.x, startPos.z];
            openNodes.Add(currentNode);

            currentNode.fCost = Int32.MaxValue;

            while (openNodes.Count > 0)
            {
                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);


                Debug.Log(currentNode.gridPos);


                // Neighbours
                for (int neighbourX = currentNode.gridPos.x - 1; neighbourX < currentNode.gridPos.x + 2; neighbourX++)
                {
                    for (int neighbourZ = currentNode.gridPos.z - 1;
                        neighbourZ < currentNode.gridPos.z + 2;
                        neighbourZ++)
                    {
                        // Check edges
                        if (neighbourX >= 0 && neighbourX <= grid.maxSizeofGrid.x && neighbourZ >= 0 &&
                            neighbourZ <= grid.maxSizeofGrid.z)
                        {
                            WorldScan.Node neighbour = grid.gridNodeReference[neighbourX, neighbourZ];
                            if (neighbour == currentNode)
                            {
                                continue;
                            }

                            if (neighbour.isBlocked || closedNodes.Contains(neighbour))
                            {
                                continue;
                            }

                            int neDistance = currentNode.gCost + (int)(10 * Vector3.Distance(currentNode.gridPos, neighbour.gridPos));
                            Debug.Log(neDistance + " neighbour distance for " + neighbour.gridPos);


                            if (neDistance < neighbour.gCost  || !openNodes.Contains(neighbour))
                            {
                                neighbour.gCost = neDistance;
                                neighbour.hCost = (int)(10 * Vector3.Distance(neighbour.gridPos, endPos));
                                neighbour.fCost = neighbour.gCost + neighbour.hCost;
                                neighbour.parent = currentNode;

                                if (!openNodes.Contains(neighbour))
                                {
                                    openNodes.Add(neighbour);
                                }
                            }
                        }
                    }

                    // if (currentNode == grid.gridNodeReference[endPos.x, endPos.z])
                    // {
                    //     break;
                    // }


                    // Remove current from open. Add current to closed
                    // Open nodes foreach looper
                    //      Find closest to endpos
                    //      Make that the new CurrentNode
                }

                for (int i = 0; i < openNodes.Count; i++)
                {
                    if (openNodes[i].fCost < currentNode.fCost)
                    {
                        currentNode = openNodes[i];
                    }
                }

                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);

                yield return new WaitForSeconds(1);
            }
        }

        // public int GetDistance(Vector3Int start, Vector3Int end)
        // {
        //     // Vector3Int distance = end - start;
        //     // distance = new Vector3Int(Mathf.Abs(distance.x), 0, Mathf.Abs(distance.z));
        //     // if (distance.x > distance.z)
        //     // {
        //     //     return distance.z * 14 + 10 * (distance.x - distance.z);
        //     // }
        //     //
        //     // return distance.x * 14 + 10 * (distance.z - distance.x);
        //    
        //       
        // }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(startPos, Vector3.one);

            Gizmos.color = Color.black;
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
        }
    }
}