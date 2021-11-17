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
        [SerializeField]
        private WorldScan grid;

        private List<WorldScan.Node> openNodes = new List<WorldScan.Node>();
        private List<WorldScan.Node> closedNodes = new List<WorldScan.Node>();


        [SerializeField] private Vector3Int currentPos;

        private void Awake()
        {
            // grid = FindObjectOfType<WorldScan>();
        }

        private void Start()
        {
            currentPos = grid.startPos;
            openNodes.Clear();
            closedNodes.Clear();
            FindPath();
        }

        void FindPath()
        {
            WorldScan.Node currentNode = grid.gridNodeReference[currentPos.x, currentPos.z];
            openNodes.Add(currentNode);

            // while (currentNode != grid.gridNodeReference[grid.endPos.x, grid.endPos.z])
            {
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
                            }
                        }
                    }
                }
                
                // Remove current from open. Add current to closed
                // Open nodes foreach looper
                //      Find closest to endpos
                //      Make that the new CurrentNode
            }
        }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(currentPos, Vector3.one);

            foreach (WorldScan.Node openNode in openNodes)
            {
                Gizmos.DrawCube(openNode.gridPos, Vector3.one);
            }
            foreach (WorldScan.Node closedNode in closedNodes)
            {
                Gizmos.DrawCube(closedNode.gridPos, Vector3.one);
            }
        }
    }
}