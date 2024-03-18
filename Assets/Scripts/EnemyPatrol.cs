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

    private NavMeshAgent agent;

    private void Start()
    {
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
       

        if (!enemyManager.isChasing && !enemyManager.isPossessed)
        {
         
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
}
