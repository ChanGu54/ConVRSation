using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Navigation 관련 기능을 사용할 때 필요.
using UnityEngine.AI;

public class BusAutoMoving : MonoBehaviour
{
    // 목표 지점
    public Transform[] target;
    NavMeshAgent agent;
    int flag = 0;

    void Start()
    {
        // 해당 개체의 NavMeshAgent 를 참조합니다.
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 매프레임마다 목표지점으로 이동합니다.
        flag = flag % target.Length;

        agent.SetDestination(target[flag].position);


        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                flag++;
            }
        }
    }
}