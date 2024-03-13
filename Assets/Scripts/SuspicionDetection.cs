using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionDetection : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private GameObject targetObject;
    private EnemyManager enemyManager;
    private EnemyManager parentManager;

    private float SusTimer = 0f;
    [SerializeField] private float TimerMax = 5f;

    public enum TagOption
    {
        Janitor,
        Guard,
        Scientist,
        IT,
        Null
    }

    public TagOption targetTag1;
    public TagOption targetTag2;

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
            if (targetObject.tag == targetTag1.ToString() || targetObject.tag == targetTag2.ToString()) 
            {
                
                enemyManager = targetObject.GetComponent<EnemyManager>();

                if (enemyManager.possessed)
                {
                    Debug.Log("Suspicion Logic");
                    SusTimer += Time.deltaTime;

                    if (SusTimer >= TimerMax)
                    {
                        parentManager.chasePlayer = true;
                    }
                    else
                    {
                        parentManager.chasePlayer = false;
                    }
                }
                else
                {
                    SusTimer = 0f;
                }
            }
        }
    }
}
