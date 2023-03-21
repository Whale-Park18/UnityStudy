using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDamp : MonoBehaviour
{
    public UnityEngine.Transform startPoint;
    public UnityEngine.Transform endPoint;
    public UnityEngine.Transform lerpPoint;
    public float moveTime;
    public float smoothTime = 0.1f;

    private IEnumerator Move()
    {
        Vector3 velocity = Vector3.zero;
        lerpPoint.position = startPoint.position;
        
        for (float time = 0; time < moveTime; time += Time.deltaTime)
        {
            lerpPoint.position = Vector3.SmoothDamp(lerpPoint.position, endPoint.position, ref velocity, smoothTime);

            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Space Key Down");
            StartCoroutine(Move());
        }
    }
}
