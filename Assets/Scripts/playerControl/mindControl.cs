using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mindControl : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;
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
        
            Debug.Log("T pressed");
            if (!isMindControl)
            {
                if (fieldOfView.targetObject != null) { 
                
                movementmanager.rb.velocity = new Vector2(0, 0);
                movementmanager.target = fieldOfView.targetObject;
                isMindControl = true;

                enemymanager = movementmanager.target.GetComponent<EnemyManager>();
                enemymanager.isPossessed = true;
            
                }
            }
            else
            {
                movementmanager.rb.velocity = new Vector2(0, 0);
               
                isMindControl = false;

                enemymanager.isPossessed = false;
                enemymanager = null;
                movementmanager.target = movementmanager.player;
            }
        
    }
    
    void Start(){
        movementmanager.target = movementmanager.player;
    }

    void FixedUpdate()
    {
    
    }
}
