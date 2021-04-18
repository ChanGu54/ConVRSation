using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public Transform targetTr;
    private Vector3 targetPos;
    private Vector3 prevPos;

    private float distTarget;
    private float distPrev;

    private float lerpDist;
    public float lerpRate;

    public float cameraDist;

    public Vector3 rotationVal;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
        lerpDist = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = targetTr.forward * cameraDist;    // 거리벡터(forward는 Y축 기준으로 앞을 보고있는 벡터. 필요에따라 수정 필요)
        Quaternion rotate = Quaternion.Euler(rotationVal);  // 회전할 각도(Y축 기준 회전. 필요에따라 수정 필요)
        Vector3 targetPoint = rotate * distance;    // 원점을 기준으로 거리와 각도를 연산한 후, 벡터
        targetPos = targetTr.position + targetPoint;    // 중심이 되는 오브젝트에서 해당 거리와 각도만큼 이동한 곳의 좌표.

        distTarget = Vector3.Distance(targetPos, transform.position);

        if (distTarget == 0)
            return;

        else
        {
            distPrev = Vector3.Distance(prevPos, targetPos);

            if (distPrev == 0 && lerpDist < 1)
            {
                lerpDist += lerpRate;
                transform.position = Vector3.Lerp(prevPos, targetPos, lerpDist);
            }
            else if (!(distPrev == 0))
            {
                lerpDist = lerpRate;
                transform.position = Vector3.Lerp(prevPos, targetPos, lerpRate);
            }

            prevPos = transform.position;
        }
    }
}