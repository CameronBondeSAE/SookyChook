using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rob
{
    public class WorldScan : MonoBehaviour
    {
        public class Node
        {
            public Vector3Int gridPos;
            public bool isBlocked;
            public int fCost;
            public int gCost;
            public int hCost;
            public Node parent;
        }

        public Node[,] gridNodeReference;
        public Vector3Int maxSizeofGrid;
        
        public Vector3Int gridSpacing;
        public LayerMask layer;
        public Vector3Int startPos;
        public Vector3Int endPos;
        public List<Node> open;


        private void Start()
        {
            gridNodeReference = new Node[maxSizeofGrid.x, maxSizeofGrid.z];
            ScanWorld();
        }

        private void ScanWorld()
        {
            for (int x = 0; x < maxSizeofGrid.x; x++)
            {
                for (int z = 0; z < maxSizeofGrid.z; z++)
                {
                    gridNodeReference[x, z] = new Node();
                    gridNodeReference[x, z].gridPos = new Vector3Int(x, 0, z);

                    if (Physics.CheckBox(new Vector3(x * gridSpacing.x, 0, z * gridSpacing.z),
                        new Vector3(gridSpacing.x, gridSpacing.y, gridSpacing.z), Quaternion.identity, layer))
                    {
                        gridNodeReference[x, z].isBlocked = true;
                    }
                }
            }
        }


        private void OnDrawGizmos()
        {
            if (gridNodeReference != null)
            {
                for (int x = 0; x < maxSizeofGrid.x; x++)
                {
                    for (int z = 0; z < maxSizeofGrid.z; z++)
                    {
                        if (gridNodeReference[x, z].isBlocked)
                        {
                            Gizmos.color = Color.red;
                            Gizmos.DrawCube(new Vector3(x, 0, z), Vector3.one);
                        }
                        else
                        {
                            Gizmos.color = Color.magenta;
                            Gizmos.DrawCube(startPos, Vector3.one);

                            Gizmos.color = Color.blue;
                            Gizmos.DrawCube(endPos, Vector3.one);

                            Gizmos.color = Color.green;
                            Gizmos.DrawCube(new Vector3(x, 0, z), Vector3.one);
                        }
                    }
                }
            }
        }
    }
}