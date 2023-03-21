using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.Raycast
{
    public class SphereCast : MonoBehaviour
    {
        public float maxDistance = 30;

        private bool success;
        private RaycastHit hitInfo;
        private float sphereRadius;

        // Update is called once per frame
        void Update()
        {
            sphereRadius = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z) / 2;

            success = Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hitInfo, maxDistance);
            if (success)
            {
                //print(gameObject.name + ": " + hitInfo.collider.name);
            }
        }

        private void OnDrawGizmos()
        {
            if(success)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawRay(transform.position, transform.forward * hitInfo.distance);
                Gizmos.DrawWireSphere(transform.position + transform.forward * hitInfo.distance, sphereRadius);
            }
        }
    }
}