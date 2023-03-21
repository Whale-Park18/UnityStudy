using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

namespace WhalePark18.Raycast
{
    public class Raycast : MonoBehaviour
    {
        public float maxDistance = 30;
        public LayerMask layerMask;

        private bool success;
        private RaycastHit hitInfo;

        private void Update()
        {
            success = Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance, layerMask);
            if (success)
            {
                //print(gameObject.name + ": " + hitInfo.collider.name);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, transform.forward * hitInfo.distance);
        }
    }
}