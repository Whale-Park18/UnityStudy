using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovementController : MonoBehaviour
{
    private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 newPosition = new Vector3(worldPosition.x, 0f, worldPosition.z);

            transform.position = newPosition;
        }
    }
}
