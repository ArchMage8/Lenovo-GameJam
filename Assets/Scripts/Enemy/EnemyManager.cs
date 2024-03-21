using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;
    [HideInInspector] public bool isPossessed;
    public Vector3 initialPosition;
    private NavMeshAgent agent;
    private DetectionSystem detectionSystem;
    [HideInInspector] public bool isChasing;
    [HideInInspector] public bool isPatrolling = true;

    private void Awake()
    {
        initialPosition = transform.position;
        detectionSystem = GetComponent<DetectionSystem>();
        agent = GetComponent<NavMeshAgent>();   
    }
    private void Start(){
        isPatrolling = true;
    }
    private void Update()
    {   
        // Debug.Log(name + isChasing);

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
