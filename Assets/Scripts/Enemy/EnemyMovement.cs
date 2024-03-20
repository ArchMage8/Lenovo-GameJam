using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

    
    }

    private void Update()
    {
       SetPlayerRotation();
    }

    private void SetPlayerRotation()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            Vector3 direction = agent.velocity.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }

    }

    public void Transform_Movement(Transform target)
    {
        if (agent.enabled)
        {
            agent.SetDestination(target.position);
        }
    }

    public void Position_Movement(Vector3 target)
    {
        if (agent.enabled)
        {
            agent.SetDestination(target);
        }
    }
}
