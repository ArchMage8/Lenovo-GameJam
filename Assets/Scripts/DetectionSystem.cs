using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;
    private GameObject TargetObject;
    private GameObject PlayerObject;

    private EnemyManager enemyManager;
    private EnemyMovement enemyMovement;
    private bool suspicionBool;
    private bool hasBeenCalled = false;
    private bool hunting = false;
    private float thispos;

    private Vector3 tempvalue;
    public enum EnemyTags
    {
        Janitor,
        Guard,
        Scientist,
        IT,
        Null
    }

    public EnemyTags targetTag1;
    public EnemyTags targetTag2;

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyMovement = GetComponent<EnemyMovement>();

        PlayerObject = GameObject.Find("Player");
    }

    public void Update()
    {   
        if (fieldOfView.targetObject != null)
        {
            hasBeenCalled = true;
            TargetObject = fieldOfView.targetObject;

            if (TargetObject.CompareTag("Player"))
            {
               
                enemyMovement.Transform_Movement(TargetObject.transform);
            }

            else if (TargetObject.tag == targetTag1.ToString() || TargetObject.tag == targetTag2.ToString())
            {
                
                enemyManager = TargetObject.GetComponent<EnemyManager>();

                if (enemyManager.isPossessed)
                {
                  
                    enemyMovement.Transform_Movement(TargetObject.transform);
                    StartCoroutine(SusCheck());
                    
                }
            }

            
        }

        else if(fieldOfView.targetObject == null && hasBeenCalled)
        {
            if (!suspicionBool)
            {
               
                enemyMovement.Transform_Movement(transform);
                StartCoroutine(QuickReturn());
            }

            hasBeenCalled = false;
        }

      
        
    }

    private IEnumerator SusCheck()
    {
        yield return new WaitForSeconds(5f);


        enemyMovement.Position_Movement(PlayerObject.transform.position);
        suspicionBool = true;
        StartCoroutine(WaitingAround());
    }

    private IEnumerator WaitingAround()
    {
        
        yield return new WaitForSeconds(25f);
        
        enemyMovement.Position_Movement(enemyManager.initialPosition);
        suspicionBool = false;

        hunting = false;
    }private IEnumerator QuickReturn()
    {
        
        yield return new WaitForSeconds(3f);
        
        enemyMovement.Position_Movement(enemyManager.initialPosition);
        suspicionBool = false;

        hunting = false;
    }
}
