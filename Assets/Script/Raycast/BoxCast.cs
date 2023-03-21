using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WhalePark18.Raycast
{
    public class BoxCast : MonoBehaviour
    {
        public float maxDistance = 30;

        private bool success;
        private RaycastHit hitInfo;

        // Update is called once per frame
        void Update()
        {
            success = Physics.BoxCast(transform.position, transform.localScale / 2, transform.forward, out hitInfo, transform.rotation, maxDistance);
            if (success)
            {
                //print(gameObject.name + ": " + hitInfo.collider.name);
            }
        }

        private void OnDrawGizmos()
        {
            if (success)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawRay(transform.position, transform.forward * hitInfo.distance);
                Gizmos.DrawWireCube(transform.position + transform.forward * hitInfo.distance, transform.localScale);
            }
        }
    }
}