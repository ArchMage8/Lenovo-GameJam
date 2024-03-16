using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mindControl : MonoBehaviour
{
    [SerializeField] PlayerFOV fieldOfView;
    [SerializeField] movementManager movementmanager;

    private EnemyManager enemymanager;

    private PlayerControls input = null;

    private Rigidbody2D rb;
    
    public float speed = 5f;

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
                movementmanager.target = fieldOfView.targetObject;
                isMindControl = true;

                enemymanager = movementmanager.target.GetComponent<EnemyManager>();
                enemymanager.isPossessed = true;
            }
        }
        else if (isMindControl)
        {
            movementmanager.rb.velocity = new Vector2(0, 0);
            isMindControl = false;
            movementmanager.target = movementmanager.player;

            enemymanager.isPossessed = false;
            enemymanager = null;
            
        }
    }
    
    void Start(){
        movementmanager.target = movementmanager.player;
    }

    void FixedUpdate()
    {
        if(fieldOfView.targetObject != null){  
          enemymanager = fieldOfView.targetObject.GetComponent<EnemyManager>();
        }
    }
}
