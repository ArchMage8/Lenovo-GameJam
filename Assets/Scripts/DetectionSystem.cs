using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;
    private GameObject TargetObject;
    private EnemyManager enemyManager;

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


    public void Update()
    {
        if (fieldOfView.targetObject != null)
        {
            TargetObject = fieldOfView.targetObject;

            if (TargetObject.CompareTag("Player"))
            {
                //Set Navmesh target to the player
            }

            else if (TargetObject.tag == targetTag1.ToString() || TargetObject.tag == targetTag2.ToString())
            {
                enemyManager = TargetObject.GetComponent<EnemyManager>();

                if (enemyManager.isPossessed)
                {
                    //Make possessed enemy navmesh target
                    //Start Coroutine
                    //After waiting, make the navmesh target the Player's position at the time
                    //After reaching X distance from player postion, wait again
                    //After waiting return to initial position
                }
            }
        }

        else
        {
           //Set NavMesh target to null
           //Start Coroutine
           //After waiting set the navmesh target to the initial position
          
        }
    }
}
