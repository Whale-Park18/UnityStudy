using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject lookAtObject;
    public float smoothness = 1;
    Vector3 zPosition;
     
    void Start() {
        zPosition = transform.position -lookAtObject.transform.position;
    }
     
    void LateUpdate() {
		Vector3 detination = lookAtObject.transform.position + zPosition;
		Vector3 pos = Vector3.Lerp(transform.position, detination, Time.deltaTime * smoothness);
        
		transform.position = pos;
        transform.LookAt(lookAtObject.transform);
	}
}
