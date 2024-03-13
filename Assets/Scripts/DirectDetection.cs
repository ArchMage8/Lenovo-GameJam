using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDetection : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private GameObject targetObject;
    private EnemyManager parentManager;

    private void Awake()
    {
        fieldOfView = GetComponent<FieldOfView>();
        parentManager = GetComponentInParent<EnemyManager>();

    }

    private void Update()
    {
        if (fieldOfView.targetObject != null)
        {
            targetObject = fieldOfView.targetObject;

            if (targetObject.CompareTag("Player"))
            {
                parentManager.chasePlayer = true;
            }
            else
            {
                parentManager.chasePlayer = false;
            }
        }


    }
}
