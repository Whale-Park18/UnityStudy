using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.Raycast
{
    public class RacastAll : MonoBehaviour
    {
        public float maxDistance = 30;
        public bool isLimit = false;
        public int limit = 0;

        private bool success;
        private RaycastHit[] hitInfos;

        private void Update()
        {
            hitInfos = Physics.RaycastAll(transform.position, transform.forward, maxDistance);
            success = hitInfos.Length != 0;        

            if (isLimit == false)
            {
                foreach (RaycastHit hitInfo in hitInfos)
                {
                    //print(gameObject.name + ": " + hitInfo.collider.name);
                    UnityEngine.Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.red);
                }
            }
            else
            {
                Array.Sort(hitInfos, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));

                for (int i = 0; i < limit; i++)
                {
                    RaycastHit hitInfo = hitInfos[i];
                    //print("<color=green>" + gameObject.name + ": " + hitInfo.collider.name + "</color>");
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            if(success)
            {
                if(isLimit == false)
                {
                    foreach (RaycastHit hitInfo in hitInfos)
                    {
                        Gizmos.DrawRay(transform.position, transform.forward * hitInfo.distance);
                    }
                }
                else
                {
                    Array.Sort(hitInfos, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));

                    for (int i = 0; i < limit; i++)
                    {
                        RaycastHit hitInfo = hitInfos[i];
                        Gizmos.DrawRay(transform.position, transform.forward * hitInfo.distance);
                    }
                }
            }
        }
    }
}