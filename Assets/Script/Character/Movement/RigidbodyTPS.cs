using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

namespace WhalePark18.CharacterExample.Movement
{ 

    public class RigidbodyTPS : MonoBehaviour
    {
        public float speed = 10f;
        public float jumpPower = 3f;
        public short jumpStack = 2;
        public float dashDistanse = 5f;
        public float rotactionSpeed = 5f;
        public float dashSpeed = 20f;

        public LayerMask layer;

        private Vector3 direction;
        private short _jumpStack;
        public bool isGrounding = true;

        public float maxDistance = 0.4f;
        private float startPositionCorrection = 0.4f;

        private Rigidbody _rigidbody;
        private InputSystem _input;
        private Animator _animator;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<InputSystem>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            _jumpStack = jumpStack;
        }

        private void Update()
        {
            Direction();
            Dash();
            JumpTo();
            CheckGround();
        }

        private void FixedUpdate()
        {
            MoveTo();
        }

        private void OnDrawGizmos()
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(transform.position + (Vector3.up * startPositionCorrection), Vector3.down, out hitInfo, maxDistance))
            {
                Gizmos.color = Color.red;

                Gizmos.DrawRay(transform.position + (Vector3.up * startPositionCorrection), -transform.up * hitInfo.distance);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position + (Vector3.up * startPositionCorrection), -transform.up * maxDistance);
            }
        }

        private void Direction()
        {
            direction = new Vector3(_input.moveX, 0f, _input.moveZ).normalized;
        }

        private void MoveTo()
        {
            // ���� ȸ�� �ڵ�
            if(direction != Vector3.zero)
            {
                // ���ݴ� �������� ȸ�� �ӵ��� �������� ���� �߻�
                // ��� �������� ȸ���ؾ��� �� �������� �ʾ� �߻���
                // ������ �� �ݴ� �������� ȸ���ؾ� �Ѵٸ� Ư�� �������� ��¦ ȸ�� ���� ��� �������� ȸ���� �� ���� �� �ְԵ�
                // ���� �ٶ󺸴� ���� ��ȣ != ���ư� ���� ��ȣ
                if (Mathf.Sign(transform.forward.x) != Mathf.Sign(direction.x) || Mathf.Sign(transform.forward.z) != Mathf.Sign(direction.z))
                {
                    transform.Rotate(0, 1, 0);
                }
                transform.forward = Vector3.Lerp(transform.forward, direction, rotactionSpeed * Time.deltaTime);
                _animator.SetBool("isRuning", true);
            }
            else
            {
                _animator.SetBool("isRuning", false);
            }

            _rigidbody.MovePosition(transform.position +  direction * speed * Time.deltaTime);
        }

        private void Dash()
        {
            if(Input.GetButtonDown("Dash"))
            {
                _rigidbody.AddForce(transform.forward * dashSpeed, ForceMode.VelocityChange);
            }
        }

        private void JumpTo()
        {
            if(Input.GetButtonDown("Jump") && _jumpStack > 0)
            {
                _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                _jumpStack--;
            }
        }

        private void CheckGround()
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(transform.position + (Vector3.up * startPositionCorrection), Vector3.down, out hitInfo, maxDistance))
            {
                print(hitInfo.transform.name);
                _jumpStack = jumpStack;
            }
            else
            {
            
            }
        }
    }
}
