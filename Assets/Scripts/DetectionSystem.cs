using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
    private EnemyManager TargetManager;
    private NavMeshAgent agent;

    public bool isChasingPlayer;
    private bool test;

    private float tempvalue = float.PositiveInfinity;
    private Vector2 tempPos;

    [SerializeField] private float maxRange = 0.1f;
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
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        tempvalue = Vector2.Distance(transform.position, tempPos);
      

        if (tempvalue <= maxRange)
        {
            StartCoroutine(WaitingAround()); //Return post suspicion logic
            test = false;
        }

        if (!enemyManager.isPossessed && agent.enabled)
        {
            if (fieldOfView.targetObject != null)
            {
                hasBeenCalled = true;
                TargetObject = fieldOfView.targetObject;

                if (TargetObject.CompareTag("Player"))
                {
                    isChasingPlayer = true;
                    //Debug.Log("Movement 1");
                    enemyMovement.Transform_Movement(TargetObject.transform);
                    enemyManager.isChasing = true;
                    hunting = false;
                }

                else if (TargetObject.tag == targetTag1.ToString() || TargetObject.tag == targetTag2.ToString())
                {
                    
                    TargetManager = TargetObject.GetComponent<EnemyManager>();

                    if (TargetManager.isPossessed && !isChasingPlayer)
                    {
                        Debug.Log("Movement 2");
                        enemyManager.isChasing = true;
                        enemyMovement.Transform_Movement(TargetObject.transform);
                        StartCoroutine(SusCheck());
                        

                        suspicionBool = true;
                        hunting = false;
                        

                    }
                }
            }
            else
            {
               
                if (!hunting)
                {
                    if (hasBeenCalled)
                    {
                        if (suspicionBool)
                        {
                           
                            Debug.Log("Suspicion Return");
                            hasBeenCalled = false;
                            suspicionBool = false;
                        }
                        else
                        {
                           
                            //Debug.Log("Direct Return");
                            //StartCoroutine(QuickReturn());
                            hasBeenCalled = false;
                        }
                    }
                }
            }
        }   
    }

    private IEnumerator SusCheck()
    {

        yield return new WaitForSeconds(3f);

        if (!test)
        {
            if (fieldOfView.targetObject != null)
            {

                Debug.Log("Conditions met, chasing player");
                enemyMovement.Position_Movement(PlayerObject.transform.position);
                suspicionBool = true;

                tempPos = PlayerObject.transform.position;
                test = true;
                enemyManager.isChasing = true;
                isChasingPlayer = true;
            }
            else
            {
                Debug.Log("Returning to Patrol");
                StartCoroutine(QuickReturn());
                test = true;

            }
        }
        

       

       
    }

   
    private IEnumerator WaitingAround()
    {
        Debug.Log("Movement 5");
        yield return new WaitForSeconds(5f);

        
        isChasingPlayer = false;

        enemyManager.isChasing = false;

        hunting = false;
        tempvalue = float.PositiveInfinity;
    }
    private IEnumerator QuickReturn()
    {
        
        yield return new WaitForSeconds(3f);

        Debug.Log("Movement 6");
        enemyManager.isChasing = false;

        isChasingPlayer = false;
        hunting = false;
        tempvalue = float.PositiveInfinity;
        test = false;
    }
}
