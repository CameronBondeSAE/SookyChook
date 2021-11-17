using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Vector2Int beginning, finish;

    private Vector3 beginningPos;

    private int distanceX;
    private int distanceY;

    //ref to Grid
    public List<ScanningGrid.Node> openSet = new List<ScanningGrid.Node>();
    public List<ScanningGrid.Node> closedSet = new List<ScanningGrid.Node>();


    private ScanningGrid grid;

    private void Start()
    {
        grid = GetComponent<ScanningGrid>();
        FindPath(beginning, finish);
    }

    private void Update()
    {
    }

    //get start and finish points in Node Space
    void FindPath(Vector2Int start, Vector2Int end)
    {
        ScanningGrid.Node endNode = grid.grid[end.x, end.y];
        ScanningGrid.Node currentNode = grid.grid[start.x, start.y];

        openSet.Add(currentNode);
        currentNode.gCost = 0;

        //loop seems not to function properly. Unity crashes
        while (currentNode != grid.grid[end.x, end.y])
        {
            int currentLowestFCost = currentNode.fCost;
            foreach (var node in openSet)
            {
                if (node.fCost <= currentNode.fCost)
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
            for (int x = (currentNode.coords.x - 1); x < (currentNode.coords.y + 2); x++)
            {
                for (int y = (currentNode.coords.y - 1); y < (currentNode.coords.y + 2); y++)
                {
                    if (x > 0 && x <= grid.gridSizeX && y > 0 && y <= grid.gridSizeY)
                    {
                        //neighbour location
                        ScanningGrid.Node neighbour = grid.grid[x, y];
                        neighbour.coords = new Vector2Int(x, y);

                        /*if (neighbour.isBlocked || closedSet.Contains(neighbour))
                        {
                            continue;
                        }*/

                        //
                        
                        int newGCostToNeighbour = currentNode.gCost + GetDistance(currentNode.coords, neighbour.coords);


                        //gCost : running total of cost FROM the start location
                        if (newGCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            //current gCost set to previous gCost plus (movement to neighbour) cost
                            neighbour.gCost = newGCostToNeighbour;
                            neighbour.hCost = GetDistance(neighbour.coords, end);

                            currentNode.hCost = GetDistance(currentNode.coords, end);

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
    }


    public int GetDistance(Vector2Int start, Vector2Int end)
    {
        Vector2Int distance = end - start;


        distance = new Vector2Int(Mathf.Abs(distance.x), Mathf.Abs(distance.y));

        if (distance.x < distance.y)
        {
            return (14 * distance.y) + (10 * distance.x);
        }

        return 14 * distance.x + 10 * distance.y;
    }

    void RetracePath(ScanningGrid.Node startNode, ScanningGrid.Node endNode)
    {
        List<ScanningGrid.Node> path = new List<ScanningGrid.Node>();
        ScanningGrid.Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
            for (int x = 0; x < grid.gridSizeX; x++)
            {
                for (int y = 0; y < grid.gridSizeY; y++)
                {
                    /*if (grid.grid[x, y] != null)
                    {*/
                    if (openSet.Contains(grid.grid[x, y]))
                    {
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawCube(new Vector3(x, y, 0), Vector3.one * (1 - 0.1f));
                    }
                    //}

                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(new Vector3(beginning.x, beginning.y, 0), Vector3.one * (01 - .01f));

                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(new Vector3(finish.x, finish.y, 0), Vector3.one * (1 - 0.1f));
                }
            }
    }
}