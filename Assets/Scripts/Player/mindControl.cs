using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mindControl : MonoBehaviour
{
    [HideInInspector] public PlayerFOV fieldOfView;
    [SerializeField] movementManager movementmanager;

    private EnemyManager enemymanager;
    public float maxDistance = 50f;

    private PlayerControls input = null;

    public bool isMindControl;

    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Control.mindControl.performed += OnMindControllPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.mindControl.performed -= OnMindControllPerformed;
    }
    private void OnMindControllPerformed(InputAction.CallbackContext context)
    {
        
        if (!isMindControl && fieldOfView.targetObject != null)
        { 
            if(!enemymanager.isChasing){

                movementmanager.rb.velocity = new Vector2(0, 0);
                movementmanager.target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                
                
                movementmanager.target = fieldOfView.targetObject;

                movementmanager.target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                // movementmanager.rb.bodyType = RigidbodyType2D.Dynamic;
                isMindControl = true;

                enemymanager = movementmanager.target.GetComponent<EnemyManager>();
                enemymanager.isPossessed = true;
                enemymanager.isPatrolling = false;
            }
        }
        else if (isMindControl)
        {
            backToPlayer();
        }
    }
    
    void Start(){
        movementmanager.target = movementmanager.player;
        fieldOfView = GetComponent<PlayerFOV>();
    }

    void FixedUpdate()
    {
        //asign enemy manager if player detect other NPC within FOV range
        if(fieldOfView.targetObject != null){  
          enemymanager = fieldOfView.targetObject.GetComponent<EnemyManager>();
        }

        rangeLimit();
    }

    void backToPlayer(){
        movementmanager.rb.velocity = new Vector2(0, 0);
        movementmanager.target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        isMindControl = false;
            
        movementmanager.target = movementmanager.player;

        movementmanager.target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        StartCoroutine(Timer());
       
    }

    void rangeLimit(){
        float distance = Vector2.Distance(movementmanager.target.transform.position, movementmanager.player.transform.position);

        // Debug.Log(distance);

        if(maxDistance < distance){
            backToPlayer();
        }
    }

    private IEnumerator Timer()
    {
        enemymanager.isPossessed = false;
        yield return new WaitForSeconds(5f);
        if(!isMindControl && !enemymanager.isChasing){            
            enemymanager.isPatrolling = true;
            enemymanager = null;
        }
    }
}
