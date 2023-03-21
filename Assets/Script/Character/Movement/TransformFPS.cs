using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WhalePark18.CharacterExample.Movement
{
    public class TransformFPS : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private InputSystem _input;

        #region Camera Movement Variables

        public Camera fpsCamera;

        [Range(0f, 1f)]
        public float cameraRotationSpeed = 1f;
        public float maxLookAngle = 90f;

        #endregion

        #region Movment Variables

        public bool playerCanMove = true;
        public float velocity;

        #endregion

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            #region Camera



            #endregion

            #region Movment

            if (playerCanMove)
            {
                Vector3 moveVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
                
                if(moveVelocity != Vector3.zero)
                {
                    Vector3.MoveTowards(transform.position, transform.TransformDirection(moveVelocity + fpsCamera.transform.forward), Time.deltaTime);
                }
            }

            #endregion
        }



    }
}