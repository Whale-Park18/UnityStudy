using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhalePark18.CharacterExample
{
    public class InputSystem : MonoBehaviour
    {
        [SerializeField] private Vector2 move;
        [SerializeField] private Vector2 mouse;

        public float moveX { get { return move.x; } }
        public float moveZ { get { return move.y; } }
        public float mouseX { get { return mouse.x; } }
        public float mouseY { get { return mouse.y; } }

        private void Update()
        {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            mouse.x = Input.GetAxisRaw("Mouse X");
            mouse.y = Input.GetAxisRaw("Mouse Y");
        }
    }
}
