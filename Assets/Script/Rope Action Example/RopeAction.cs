using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAction : MonoBehaviour
{
    /// <summary>
    /// 1. 플레이어에서 Raycat 발사
    /// 2. 부착 가능한 오브젝트이면 LineRenderer를 이용하여 로프 생성
    /// 3. Raycast hit 위치에 스프링 조인트 앵커 부착
    /// </summary>
    /// 

    public Transform player;
    private Camera cam;
    private RaycastHit hit;
    public LayerMask GrapplingObj;
    private LineRenderer lineRenderer;
    private bool isGrapping;
    public Transform muzzle;
    private Vector3 targetPosition;

    SpringJoint springJoint;
    public float spring = 10f;
    public float damper = 10f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RopeShoot();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            EndShoot();
        }

        DrawRope();
    }

    private void RopeShoot()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, GrapplingObj))
        {
            isGrapping = true;

            targetPosition = hit.point;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, muzzle.position);
            lineRenderer.SetPosition(1, hit.point);

            springJoint = player.GetComponent<SpringJoint>();
            springJoint.autoConfigureConnectedAnchor = false; // 앵커위치 자동 설정, 직접 설정할 것이기 때문에 비활성화
            springJoint.connectedAnchor = targetPosition;

            float distance = Vector3.Distance(transform.position, targetPosition);

            springJoint.maxDistance = distance * 0.7f;
            springJoint.minDistance = distance * 0.5f;
            springJoint.spring = spring;
            springJoint.damper = damper;
        }
    }

    private void EndShoot()
    {
        isGrapping = false;

        lineRenderer.positionCount = 0;
        springJoint.breakTorque = 1f;
       // Destroy(springJoint);
    }

    private void DrawRope()
    {
        if(isGrapping)
        {
            lineRenderer.SetPosition(0, muzzle.position);
            transform.LookAt(targetPosition);
        }
    }
}
