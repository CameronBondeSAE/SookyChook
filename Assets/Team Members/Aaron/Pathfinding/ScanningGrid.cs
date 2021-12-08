using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

public class ScanningGrid : ManagerBase<ScanningGrid>
{
    public LayerMask layer;
    public Vector3Int worldSize;

    //[ReadOnly]
    public Node[,] grid;

    public int gridSizeX;
    public int gridSizeZ;

    private int nodeSize = 1;


    public class Node
    {
        private Vector3 worldPosition;
        public bool isBlocked;

        public int gridX;
        public int gridZ;

        public Vector3Int coords;

        public int gCost;
        public int hCost;

        public Node parent;

        public int fCost
        {
            get { return gCost + hCost; }
        }
    }

    private void Start()
    {
        //still need to attach grid to level btw

        //how many grid spots in the world
        gridSizeX = Mathf.RoundToInt(worldSize.x / nodeSize);
        gridSizeZ = Mathf.RoundToInt(worldSize.z / nodeSize);

        grid = new Node [worldSize.x, worldSize.z];
        CreateGrid();
        List<Node> neighbours = new List<Node>();
    }

    void CreateGrid()
    {
        //Vector3 worldBottomLeft = transform.position - Vector3.right*grid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                grid[x, z] = new Node();
                if ((Physics.CheckBox(new Vector3(x, 0, z),
                    new Vector3(nodeSize, 0, nodeSize), Quaternion.identity, layer)))
                {
                    grid[x, z].isBlocked = true;
                }
            }
        }
    }

    //get node from world point
    public Node NodeFromWorldPos(Vector3 worldPos)
    {
        //get percentage of location across world grid
        float percentX = (worldPos.x + worldSize.x / 2) / worldSize.x;
        float percentZ = (worldPos.z + worldSize.z / 2) / worldSize.z;

        //clamp between 0 and 1 
        percentX = Mathf.Clamp01(percentX);
        percentZ = Mathf.Clamp01(percentZ);

        //getting world pos locations from percentage to int
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int z = Mathf.RoundToInt((gridSizeZ - 1) * percentZ);

        //sends world location to the 2D array
        return grid[x, z];
    }

    public List<Node> path;

    private void OnDrawGizmos()
    {
        /*//grid parameters outline
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, worldSize.y, worldSize.z));*/

        //drawing and filling grid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                if (grid != null && grid[x, z].isBlocked)
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawCube(new Vector3(x, 0, z), Vector3.one * (nodeSize - 0.1f));
                }
                /*if(x == 0 || z == 0)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawCube(new Vector3(x, 0, z), Vector3.one * (nodeSize - 0.1f));
                }*/
            }
        }
    }
}