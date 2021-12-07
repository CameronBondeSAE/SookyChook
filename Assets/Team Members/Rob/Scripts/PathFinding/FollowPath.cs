using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rob
{
    public class FollowPath : MonoBehaviour
    {
        //public PathFinding pathFinder;
        public Vector3 target;
        public float distanceToNode;
        public int currentIndex;
        List<WorldScan.Node> path;


        public void Start()
        {
            PathFinding.Instance.startPos = PathFinding.Instance.ConvertWorldToGridSpace(transform.position);
            PathFinding.Instance.endPos = PathFinding.Instance.ConvertWorldToGridSpace(target);
            path = PathFinding.Instance.FindPath().ToList();
            currentIndex = 0;
            target = PathFinding.Instance.ConvertGridToWorldSpace(path[0].gridPos);
        }

        public void Update()
        {
            if (PathFinding.Instance.path.Count > 0)
            {
                //HACK change later
                
                transform.Translate(0,0,Time.deltaTime * 5, Space.Self);
                if (Vector3.Distance(transform.position, target) < distanceToNode)
                {
                    currentIndex++;
                    target = PathFinding.Instance.ConvertGridToWorldSpace(path[currentIndex].gridPos);
                }
            }
            transform.LookAt(target);
        }
    }
}