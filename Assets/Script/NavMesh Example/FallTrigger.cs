using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    public Rigidbody fallObject;

    private void OnTriggerEnter(Collider other)
    {
        fallObject.useGravity = true;
    }
}
