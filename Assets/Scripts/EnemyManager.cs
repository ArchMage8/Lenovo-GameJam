using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public bool isPossessed;
    public Vector3 initialPosition;
    private NavMeshAgent agent;
    private DetectionSystem detectionSystem;

    public bool isChasing;
    public bool isPatrolling;

    private void Awake()
    {
        initialPosition = transform.position;
        detectionSystem = GetComponent<DetectionSystem>();
        agent = GetComponent<NavMeshAgent>();   
    }

    private void Update()
    {   
        if (isPossessed)
        {
            agent.enabled = false;  
        }

        else if (!isPossessed)
        {
            agent.enabled = true;            
        }
    }
}
