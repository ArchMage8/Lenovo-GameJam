using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{

    private EnemyManager enemyManager;

    private void Start()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
    }

    private void Update()
    {
        if(enemyManager != null && enemyManager.chasePlayer)
        {
            Debug.Log("ChasingPlayer");
        }
    }
}
