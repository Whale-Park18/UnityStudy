using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalcDegree : MonoBehaviour
{
    public Transform baseTransform;
    public Transform offsetTransform;
    public Transform target;
    public Text degreeText;

    void Update()
    {
        // 1. 상대적 거리 계산
        // 2. Atan2를 이용해 라디안 계산
        // 3. 라디안 -> 호도법 변환 상수를 이용해 라디안을 호도법으로 변환
        Vector3 relative = target.position - baseTransform.position;
        float radian = Mathf.Atan2(relative.x, relative.z);
        float degree = radian * Mathf.Rad2Deg;

        degreeText.text = degree + "°";

        // 구한 각도만큼 회전
        offsetTransform.rotation = Quaternion.Euler(0f, degree, 0f);
    }
}
