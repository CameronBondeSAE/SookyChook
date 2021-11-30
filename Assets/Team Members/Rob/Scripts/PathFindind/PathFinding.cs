using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rob
{
    public class PathFinding : MonoBehaviour
    {
        
        // use these  bools to auto start or to see all gizmos (open/closed nodes)
        public bool autoStart;
        public bool debug;
        
        [Tooltip("Must set endPos to a different position than startPos")] 
        [SerializeField] private Vector3Int endPos;
        [Tooltip("Must set startPos or it will default to 0,0,0")]
        [SerializeField] private Vector3Int startPos;
        
        [SerializeField] private WorldScan grid;

        public List<WorldScan.Node> openNodes = new List<WorldScan.Node>();
        public List<WorldScan.Node> closedNodes = new List<WorldScan.Node>();
        List<WorldScan.Node> path = new List<WorldScan.Node>();


        private WorldScan.Node endNode;
        WorldScan.Node startNode;
        WorldScan.Node currentNode;
        WorldScan.Node neighbour;

        

        private void Awake()
        {
            // grid = FindObjectOfType<WorldScan>();
        }

        private void Start()
        {
            endNode = grid.gridNodeReference[endPos.x, endPos.z];
            openNodes.Clear();
            closedNodes.Clear();
            if (autoStart)
            {
                //if you want to see that algorithm work, comment out find path and reinstate the coroutine
                
                //StartCoroutine(FindPath());
                FindPath();
            }
        }
        
        //must comment out public void and uncomment Ienumerator to visualise

        //IEnumerator FindPath()
        public void FindPath()
        {
            startNode = grid.gridNodeReference[startPos.x, startPos.z]; //set the start node
            openNodes.Add(startNode); //add 1st node to the open list

            while (openNodes.Count > 0)
            {
                currentNode = openNodes[0];
                for (int i = 1; i < openNodes.Count; i++)
                {
                    if (openNodes[i].fCost < currentNode.fCost || openNodes[i].fCost == currentNode.fCost &&
                        openNodes[i].hCost < currentNode.hCost)
                    {
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

            Gizmos.color = Color.white;
            Gizmos.DrawCube(endPos, Vector3.one);

            foreach (WorldScan.Node nodePath in path)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(nodePath.gridPos, Vector3.one);
            }

            if (debug)
            {
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
}