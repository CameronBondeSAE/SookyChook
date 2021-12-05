using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aaron;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Aaron
{
    [CustomEditor(typeof(Pathfinding))]

    public class PathfindingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Recalculate Path"))
            {
                ((Pathfinding)target).FindPath(((Pathfinding)target).beginning, ((Pathfinding)target).finish);
            }
        }
}
}