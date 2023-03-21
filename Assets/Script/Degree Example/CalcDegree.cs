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
        // 1. ����� �Ÿ� ���
        // 2. Atan2�� �̿��� ���� ���
        // 3. ���� -> ȣ���� ��ȯ ����� �̿��� ������ ȣ�������� ��ȯ
        Vector3 relative = target.position - baseTransform.position;
        float radian = Mathf.Atan2(relative.x, relative.z);
        float degree = radian * Mathf.Rad2Deg;

        degreeText.text = degree + "��";

        // ���� ������ŭ ȸ��
        offsetTransform.rotation = Quaternion.Euler(0f, degree, 0f);
    }
}
