using System.Collections;
using System.Collections.Generic;
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
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        if (!enemyManager.isPossessed && agent.enabled)
        {
            if (fieldOfView.targetObject != null)
            {
                hasBeenCalled = true;
                TargetObject = fieldOfView.targetObject;

                if (TargetObject.CompareTag("Player"))
                {

                    //Debug.Log("Movement 1");
                    enemyMovement.Transform_Movement(TargetObject.transform);
                    enemyManager.isChasing = true;
                }

                else if (TargetObject.tag == targetTag1.ToString() || TargetObject.tag == targetTag2.ToString())
                {

                    TargetManager = TargetObject.GetComponent<EnemyManager>();

                    if (TargetManager.isPossessed && !isChasingPlayer)
                    {
                        //Debug.Log("Movement 2");
                        enemyMovement.Transform_Movement(TargetObject.transform);
                        StartCoroutine(SusCheck());

                        suspicionBool = true;
                        hunting = true;
                        enemyManager.isChasing = true;

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
                            //Debug.Log("Suspicion Return");
                            hasBeenCalled = false;
                            suspicionBool = false;
                        }
                        else
                        {
                            //Debug.Log("Direct Return");
                            StartCoroutine(QuickReturn());
                            hasBeenCalled = false;
                        }
                    }
                }
            }
        }   
    }

    private IEnumerator SusCheck()
    {
        yield return new WaitForSeconds(5f);

        //Debug.Log("Movement 4");
        isChasingPlayer = true;
        enemyMovement.Position_Movement(PlayerObject.transform.position);
        suspicionBool = true;

        StartCoroutine(WaitingAround());
    }

   
    private IEnumerator WaitingAround()
    {
        
        yield return new WaitForSeconds(25f);

        //Debug.Log("Movement 5");
        isChasingPlayer = false;

        enemyManager.isChasing = false;

        hunting = false;
    }
    private IEnumerator QuickReturn()
    {
        
        yield return new WaitForSeconds(3f);

        //Debug.Log("Movement 6");
        enemyManager.isChasing = false;


        hunting = false;
    }
}
