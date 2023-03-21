using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSmoothStep : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform lerpPoint;
    public float moveTime;

    private IEnumerator Move()
    {
        for(float time = 0; time < moveTime; time += Time.deltaTime)
        {
            float smoothSetp = Mathf.SmoothStep(0, 1, time / moveTime);
            lerpPoint.position = Vector3.Lerp(startPoint.position, endPoint.position, smoothSetp);

            yield return null;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("Space Key Down");
            StartCoroutine(Move());
        }
    }
}
