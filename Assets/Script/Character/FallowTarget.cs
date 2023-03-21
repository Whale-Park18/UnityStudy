using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowTarget : MonoBehaviour
{
    public Transform targst;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - targst.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = targst.position + offset;
    }
}
