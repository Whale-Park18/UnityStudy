using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slerp : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform lerpPoint;

    [Range(0f, 1f)]
    public float t;

    private void Update()
    {
        lerpPoint.position = Vector3.Slerp(startPoint.position, endPoint.position, t);

    }
}
