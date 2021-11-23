using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rob
{
    public class PathFinding : MonoBehaviour
    {
        public Vector3Int endPos;
        public Vector3Int startPos;

        [SerializeField] private WorldScan grid;

        private List<WorldScan.Node> openNodes = new List<WorldScan.Node>();
        private List<WorldScan.Node> closedNodes = new List<WorldScan.Node>();
        private int endCost;
        private int lowestFCost;


        [SerializeField] private Vector3Int currentPos;

        private void Awake()
        {
            // grid = FindObjectOfType<WorldScan>();
        }

        private void Start()
        {
            currentPos = startPos;
            endCost = endPos.x + endPos.z;
            openNodes.Clear();
            closedNodes.Clear();
            FindPath();
        }

        void FindPath()
        {
            WorldScan.Node currentNode = grid.gridNodeReference[currentPos.x, currentPos.z];
            openNodes.Add(currentNode);

            //while (currentNode != grid.gridNodeReference[endPos.x, endPos.z])

            // Neighbours
            for (int neighbourX = currentNode.gridPos.x - 1; neighbourX < currentNode.gridPos.x + 2; neighbourX++)
            {
                for (int neighbourZ = currentNode.gridPos.z - 1;
                    neighbourZ < currentNode.gridPos.z + 2;
                    neighbourZ++)
                {
                    // Check edges
                    if (neighbourX >= 0 && neighbourX <= grid.gridSpacing.x && neighbourZ >= 0 &&
                        neighbourZ <= grid.gridSpacing.z)
                    {
                        WorldScan.Node neighbour = grid.gridNodeReference[neighbourX, neighbourZ];
                        if (!neighbour.isBlocked || !closedNodes.Contains(neighbour))
                        {
                            WorldScan.Node node = neighbour;
                            openNodes.Add(node);
                            Debug.Log(openNodes.Count);
                        }

                        if (neighbour.isBlocked && !closedNodes.Contains(neighbour))
                        {
                            WorldScan.Node node = neighbour;
                            closedNodes.Add(node);
                        }
                    }
                }

                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);


                // foreach (WorldScan.Node openNode in openNodes)
                // {
                //     if (openNode.)
                //     {
                //     }
                // }


                foreach (WorldScan.Node openNode in openNodes)
                {
                }


                // Remove current from open. Add current to closed
                // Open nodes foreach looper
                //      Find closest to endpos
                //      Make that the new CurrentNode
            }
        }

        public int GetDistance(Vector3Int start, Vector3Int end)
        {
            Vector3Int distance = end - start;
            distance = new Vector3Int(Mathf.Abs(distance.x), 0, Mathf.Abs(distance.y));
            if (distance.x > distance.z)
            {
                return distance.z * 14 + 10 * (distance.x - distance.z);
            }

            return distance.x * 14 + 10 * (distance.z - distance.x);
        }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(currentPos, Vector3.one);

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