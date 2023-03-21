using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTimer : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform lerpPoint;
    public float moveTime;

    private IEnumerator Move()
    {
        for(float time = 0; time < moveTime; time += Time.deltaTime)
        {
            lerpPoint.position = Vector3.Lerp(startPoint.position, endPoint.position, time/moveTime);

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
