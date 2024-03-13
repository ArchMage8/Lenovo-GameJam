using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SuspicionDetection : MonoBehaviour
{
    private FieldOfView fieldOfView;
    private GameObject targetObject;
    private EnemyManager enemyManager;
    private EnemyManager parentManager;
    private ChasePlayer chasePlayer;

    private bool Watched;

    [SerializeField] private float SusTimer = 0f;
    [SerializeField] private float MaxTimer = 10f;
    

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
       chasePlayer = GetComponentInParent<ChasePlayer>();

    }

    private void Update()
    {
        if (fieldOfView.targetObject != null)
        {
            
            targetObject = fieldOfView.targetObject;
            if (targetObject.tag == targetTag1.ToString() || targetObject.tag == targetTag2.ToString()) 
            {
                
                enemyManager = targetObject.GetComponent<EnemyManager>();

                if(enemyManager.possessed == true)
                {
                    SusTimer += Time.deltaTime;
                    Watched = true;

                    parentManager.chaseTarget = true;
                    chasePlayer.SusChase = true;
                    Debug.Log("Sus Logic");

                    if (SusTimer >= MaxTimer)
                    {
                        

                        chasePlayer.SusChase = false;
                       chasePlayer.PlayerChase = true;
                        Debug.Log("Chase Logic");

                    }
                   
                }
                else
                {
                    if(Watched == true)
                    {
                        chasePlayer.PlayerChase = true;
                        Debug.Log("Chase after depossessed");
                    }
                }
            }
           
        }
        else
        {
            if (Watched)
            {
                StartCoroutine(LoseInterest());
            }
        }

        
       
    }

    private IEnumerator LoseInterest()
    {
        if (fieldOfView.targetObject == null)
        {
           
            yield return new WaitForSeconds(5f);
            Debug.Log("Lost interest");
            Watched = false;
            chasePlayer.SusChase = false;
            chasePlayer.PlayerChase = false;

        }
        else
        {
            
        }
    }
}
