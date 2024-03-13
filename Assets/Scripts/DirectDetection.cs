using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDetection : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private GameObject targetObject;
    private EnemyManager parentManager;
    private ChasePlayer chasePlayer;

    private void Awake()
    {
        fieldOfView = GetComponent<FieldOfView>();
        parentManager = GetComponentInParent<EnemyManager>();
        chasePlayer = GetComponentInParent<ChasePlayer>();

    }

    private void Update()
    {
        if (fieldOfView.targetObject != null)
        {
            targetObject = fieldOfView.targetObject;

            if (targetObject.CompareTag("Player"))
            {
                Debug.Log("Player Detect");
                chasePlayer.PlayerChase = true;
                parentManager.chaseTarget = true;
            }
        }


    }
}
