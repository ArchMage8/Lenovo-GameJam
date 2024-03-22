using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private EnemyManager enemyManager;
    private FieldOfView fieldOfView;

    [SerializeField] private GameObject[] checkpoints;
    private Vector3[] internalArray;
    [SerializeField] private float waitTime = 3f;

    private int currentIndex = 0;
    private bool isMoving = true;
    private bool isWaiting = false;
    private bool isStuck = false;
    private Vector3 lastPosition;
    private Vector3 SpawnLocation;
    private NavMeshAgent agent;

    private void Start()
    {
        SpawnLocation = transform.position;
        enemyManager = GetComponent<EnemyManager>();
        fieldOfView = GetComponentInChildren<FieldOfView>();

        agent = GetComponent<NavMeshAgent>();

        //Assigning to internal array
        internalArray = new Vector3[checkpoints.Length];

        for (int i = 0; i < checkpoints.Length; i++)
        {
            internalArray[i] = checkpoints[i].transform.position;
        }
    }

    public void Update()
    {
        if(!isStuck){
            Debug.Log("it Wok");
            isStuck  = true;
            StartCoroutine(StuckCheck());
        }
        if (!enemyManager.isChasing && !enemyManager.isPossessed && enemyManager.isPatrolling)
        {
            // Debug.Log("patrolling");
            if (!isWaiting && isMoving)
            {
                if (Vector2.Distance(transform.position, internalArray[currentIndex]) <= 0.2f)
                {
                    StartCoroutine(WaitAtCheckpoint());
                }
                else
                {
                    MovetoNextDestination();
                }
            }
        }
    }

    private void MovetoNextDestination()
    {
        if (agent.enabled)
        {
            agent.SetDestination(internalArray[currentIndex]);
        }
    }

    private IEnumerator WaitAtCheckpoint()
    {
        isWaiting = true;
        isMoving = false; // Stop moving while waiting
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        isMoving = true; // Resume moving after waiting

        currentIndex = (currentIndex + 1) % internalArray.Length; // Move to the next checkpoint
        MovetoNextDestination();
    }
    private IEnumerator StuckCheck()
    {
        Debug.Log("it wok");
        yield return new WaitForSeconds(5); 
        if(transform.position == lastPosition)
        {
            OnNotMoved();
        }
        isStuck = false;
        lastPosition = transform.position;
    }
    void OnNotMoved()
    {
        transform.position = SpawnLocation;
        WaitAtCheckpoint();
    }
}
