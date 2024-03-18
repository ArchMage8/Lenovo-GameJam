using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DetectionSystem : MonoBehaviour
{
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

    [SerializeField] private float chaseDuration = 5f;
    [SerializeField] private float waitDuration = 5f;

    private GameObject Target;
    private Vector2 tempPlayerPos;
    private Transform tempTargetPos;
    private float tempDistance = float.PositiveInfinity;

    //Script references
    private FieldOfView fieldOfView;
    private EnemyMovement enemyMovement;
    private EnemyManager enemyManager;
    

    //Bools
    private bool isChasing = false;
    private bool isChasingPlayer = false;
    private bool hunting = false;
    private bool inRange = false;
    private bool possessedCheck = false;
    

 

    private void Start()
    {
        fieldOfView = GetComponentInChildren<FieldOfView>();
        enemyManager = GetComponent<EnemyManager>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        tempPlayerPos = GameObject.Find("Player").transform.position; 
        tempDistance = Vector2.Distance(transform.position, tempPlayerPos);
        Target = fieldOfView.targetObject;
        // enemyManager.isChasing = isChasing;
        // Debug.Log(name+enemyManager.isChasing); //Note : This logic works cuz if we arent chasing AND there is no target that matches constraints, we dont enter logic   
        if (inRange && hunting)
        {
            Debug.Log("Chasing Finished");
            StartCoroutine(Waiting());
        }

        if (Target != null ) //&& !isChasing) //Check if there is something infront of the NPC, and isnt currently chasing anything else
        {
            if (Target.CompareTag("Player"))    //If target is Player, chase the player
            {
                // Debug.Log("chasing player");
                Debug.Log(fieldOfView.targetObject);
                tempTargetPos = fieldOfView.targetObject.GetComponent<Transform>();
                // Target = fieldOfView.targetObject;
                enemyMovement.Transform_Movement(tempTargetPos);
                enemyManager.isChasing = true;
                // isChasing = true;
                isChasingPlayer = true;

            }

            else if ((Target.tag == targetTag1.ToString() || Target.tag == targetTag2.ToString()) && !isChasingPlayer) //Else if the target is of interest
            {
                EnemyManager TargetManager = Target.GetComponent<EnemyManager>();
                if (TargetManager != null && TargetManager.isPossessed)             //Check are they possessed?
                {
                    enemyManager.isChasing = true;
                    // hunting = true;
                    // Debug.Log("Movement 2");
                    // Target = fieldOfView.targetObject;
                    enemyMovement.Transform_Movement(Target.transform);             //Chase the target
                      //Where is the player currently
                    if(!possessedCheck){        
                        StartCoroutine(ChasePossessed());                               //Chase the Possessed NPC [Refer Chase Possessed Comments]
                    }

                }
            }

        }

        else
        {   
            if(isChasingPlayer && Target == null){
                // Debug.Log("player lose line of sight");
                StartCoroutine(Timer());
            }
            else if (!hunting && !isChasingPlayer)
            {   
                // Debug.Log("is chasing reset");
                StartCoroutine(Timer());
            }
        }
        if (tempDistance <= 0.1f)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }
        

    private IEnumerator ChasePossessed()
    {
        possessedCheck = true;
        yield return new WaitForSeconds(5f);                //Wait 5 seconds

        if(Target != null){
            if (Target.GetComponent<EnemyManager>().isPossessed)                                  //If after 5 seconds the possessed NPC is in line of sight
            {
                possessedCheck = false;
                // Debug.Log("Chasing Player");
                hunting = true;                                 //Handle the fact chasing the player means the NPC is looking at nothing
                isChasingPlayer = true;
                enemyManager.isChasing = true;
                enemyMovement.Position_Movement(tempPlayerPos); //Go to the player position
            }
        }
        else
        {
            possessedCheck = false;
            // Debug.Log("Possesed escaped");
            yield return new WaitForSeconds(5f);            //Else activate "lost target logic"
            hunting = false;
            enemyManager.isChasing = false;
        }
    }

    private IEnumerator Waiting()
    {
        Debug.Log("Movement 5");
        yield return new WaitForSeconds(5f);
        isChasingPlayer = false;                //At this point we have reached the player's pos
        enemyManager.isChasing = false;                                  //Activating return to patrol logic
        hunting = false;
        tempDistance = float.PositiveInfinity;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(4f);
        isChasingPlayer = false;
        enemyManager.isChasing = false;
    }
}
