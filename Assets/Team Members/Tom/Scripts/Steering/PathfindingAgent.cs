using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tom
{
    public class PathfindingAgent : MonoBehaviour
    {
        private PathfindingGrid grid;

        private List<PathfindingGrid.Node> openNodes = new List<PathfindingGrid.Node>();
        private List<PathfindingGrid.Node> closedNodes = new List<PathfindingGrid.Node>();

        public Vector2Int start, destination;

        public List<PathfindingGrid.Node> path = new List<PathfindingGrid.Node>();

        private void Start()
        {
            grid = FindObjectOfType<PathfindingGrid>();
            //FindPath(start, destination);
        }

        public void FindPath(Vector2Int start, Vector2Int destination)
        {
            foreach (PathfindingGrid.Node node in grid.nodes)
            {
                node.parent = null;
                node.gCost = 0;
                node.hCost = 0;
                node.fCost = 0;
            }
            openNodes.Clear();
            closedNodes.Clear();
        
            PathfindingGrid.Node currentNode = grid.nodes[start.x, start.y];
            openNodes.Add(currentNode);

            // Loops until destination has been found
            while (currentNode != grid.nodes[destination.x, destination.y])
            {
                // Find neighbour with lowest f cost
                int lowestFCost = openNodes[0].fCost;
                foreach (PathfindingGrid.Node node in openNodes)
                {
                    if (node.fCost <= lowestFCost)
                    {
                        lowestFCost = node.fCost;
                        currentNode = node;
                    }
                }

                // Close new current node
                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);

                if (currentNode == grid.nodes[destination.x, destination.y])
                {
                    break;
                }

                // Check all grid nodes +/- 1 in both directions (neighbours)
                for (int i = currentNode.coordinates.x - 1; i < currentNode.coordinates.x + 2; i++)
                {
                    for (int j = currentNode.coordinates.y - 1; j < currentNode.coordinates.y + 2; j++)
                    {
                        if (i >= grid.gridStartX && i <= grid.gridEndX && j >= grid.gridStartY && j <= grid.gridEndY)
                        {
                            PathfindingGrid.Node neighbour = grid.nodes[i, j];
                            // Ignore neighbour if blocked or closed
                            if (neighbour.blocked || closedNodes.Contains(neighbour))
                            {
                                continue;
                            }

                            int neighbourDistance = currentNode.gCost +
                                                    CalculateDistance(currentNode.coordinates, neighbour.coordinates);

                            if (neighbourDistance < neighbour.gCost || !openNodes.Contains(grid.nodes[i, j]))
                            {
                                neighbour.gCost = neighbourDistance;
                                neighbour.hCost = CalculateDistance(neighbour.coordinates, destination);
                                neighbour.fCost = neighbour.gCost + neighbour.hCost;

                                neighbour.parent = currentNode;
                                if (!openNodes.Contains(neighbour))
                                {
                                    openNodes.Add(neighbour);
                                }
                            }
                        }
                    }
                }
            }

            path.Clear();

            // Generate path based on best nodes' parents
            while (currentNode != grid.nodes[start.x, start.y])
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
        }

        public int CalculateDistance(Vector2Int start, Vector2Int end)
        {
            Vector2Int distance = end - start;
            distance = new Vector2Int(Mathf.Abs(distance.x), Mathf.Abs(distance.y)); // This removes any negative numbers
            if (distance.x > distance.y)
            {
                return distance.y * 14 + 10 * (distance.x - distance.y);
            }

            return distance.x * 14 + 10 * (distance.y - distance.x);
        }

        private void OnDrawGizmosSelected()
        {
            if (grid != null)
            {
                for (int x = grid.gridStartX; x < grid.gridEndX; x++)
                {
                    for (int z = grid.gridStartY; z < grid.gridEndY; z++)
                    {
                        if (grid.nodes[x, z] != null)
                        {
                            if (openNodes.Contains(grid.nodes[x, z]))
                            {
                                Gizmos.color = Color.green;
                                Gizmos.DrawCube(new Vector3(x, 0.5f, z), Vector3.one);
                            }
                            if (closedNodes.Contains(grid.nodes[x, z]))
                            {
                                Gizmos.color = Color.yellow;
                                Gizmos.DrawCube(new Vector3(x, 0.5f, z), Vector3.one);
                            }
                            if (path.Contains(grid.nodes[x,z]))
                            {
                                Gizmos.color = Color.blue;
                                Gizmos.DrawCube(new Vector3(x, 0.5f, z), Vector3.one);
                            }
                        }
                    }
                }
            }
        }
    }
}