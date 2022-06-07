using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 따라갈 좌표의 차이
    public Vector3 offset;

    // 따라가는 속도
    public float followSpeed = 0.15f;

    // 따라갈 오브젝트
    private GameObject target;

    void Awake()
    {
        // target을 Player로 설정
        target = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        // target 좌표에 offset을 더하여 camera_pos에 저장
        Vector3 camera_pos = target.transform.position + offset;

        // Lerp함수를 통하여 카메라 좌표와 이동할 카메라 좌표 선형보간
        Vector3 lerp_pos = Vector3.Lerp(transform.position, camera_pos, followSpeed);

        // 카메라 좌표 변경
        transform.position = lerp_pos;
    }
}
