using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLerpMovement : MonoBehaviour
{
    public float travelTime;
    public Transform target;
    private Vector3 startPosition;

    private IEnumerator Start()
    {
        Vector3 startPosition = transform.position;
        for(float currentTime = 0, percent = 0; currentTime < travelTime; currentTime += Time.deltaTime, percent = currentTime / travelTime)
        {
            transform.position = Vector3.Lerp(startPosition, target.position, percent);
            yield return null;
        }
    }
}
