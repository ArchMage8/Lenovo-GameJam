using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{

    private EnemyManager enemyManager;
    private Transform TempTarget;
    public GameObject PlayerTarget;
    private FieldOfView fieldOfView;

    public bool SusChase;
    public bool PlayerChase;

    NavMeshAgent agent;

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        agent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
       
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (fieldOfView.targetObject != null)
        {
            TempTarget = fieldOfView.targetObject.transform;
        }

            if (enemyManager != null && enemyManager.chaseTarget == true)
        {

            if (PlayerChase)
            {
                agent.SetDestination(PlayerTarget.transform.position);

            }

            else if (SusChase)
            {
                agent.SetDestination(TempTarget.position);
            }

            else if (!PlayerChase && !SusChase)
            {
                agent.SetDestination(enemyManager.initialPosition);
            }


            if (agent.velocity.magnitude > 0.1f)
            {
                Vector3 direction = agent.velocity.normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            }
        }
    }
}
