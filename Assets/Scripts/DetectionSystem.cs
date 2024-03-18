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
    private float tempDistance = float.PositiveInfinity;

    //Script references
    private FieldOfView fieldOfView;
    private EnemyMovement enemyMovement;
    private EnemyManager enemyManager;
    

    //Bools
    private bool isChasing = false;
    private bool hunting = false;
    private bool inRange = false;
    
 

    private void Start()
    {
        fieldOfView = GetComponentInChildren<FieldOfView>();
        enemyManager = GetComponent<EnemyManager>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        tempDistance = Vector2.Distance(transform.position, tempPlayerPos);

        Target = fieldOfView.targetObject;
        enemyManager.isChasing = isChasing; //Note : This logic works cuz if we arent chasing AND there is no target that matches constraints, we dont enter logic

        if (Target != null && !isChasing) //Check if there is something infront of the NPC, and isnt currently chasing anything else
        {
            if (Target.CompareTag("Player"))    //If target is Player, chase the player
            {
                Debug.Log("Movment 1");
                enemyMovement.Transform_Movement(Target.transform);
                isChasing = true;

            }

            else if (Target.tag == targetTag1.ToString() || Target.tag == targetTag2.ToString()) //Else if the target is of interest
            {
                EnemyManager TargetManager = Target.GetComponent<EnemyManager>();
                if (TargetManager != null && TargetManager.isPossessed)             //Check are they possessed?
                {
                    isChasing = true;
                    hunting = true;

                    Debug.Log("Movement 2");
                    enemyMovement.Transform_Movement(Target.transform);             //Chase the target
                    tempPlayerPos = GameObject.Find("Player").transform.position;   //Where is the player currently
                    StartCoroutine(ChasePossessed());                               //Chase the Possessed NPC [Refer Chase Possessed Comments]
                    if (inRange)
                    {
                        StartCoroutine(Waiting());
                    }

                }
            }

        }

        else
        {
            if (!hunting)
            {
                StartCoroutine(Timer());
                isChasing = false;
            }
        }
        if (tempDistance < 0.5f)
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
        
        
        yield return new WaitForSeconds(5f);                //Wait 5 seconds

        if (Target != null)                                  //If after 5 seconds the possessed NPC is in line of sight
        {
            Debug.Log("Movment 3");
            hunting = true;                                 //Handle the fact chasing the player means the NPC is looking at nothing
            enemyMovement.Position_Movement(tempPlayerPos); //Go to the player position
        }
        else
        {
            Debug.Log("Movment 4");
            yield return new WaitForSeconds(5f);            //Else activate "lost target logic"
            hunting = false;
        }
    }

    private IEnumerator Waiting()
    {
        Debug.Log("Movment 5");
        yield return new WaitForSeconds(5f);                //At this point we have reached the player's pos
        isChasing = false;                                  //Activating return to patrol logic
        hunting = false;
        tempDistance = float.PositiveInfinity;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(5f);
    }
}
