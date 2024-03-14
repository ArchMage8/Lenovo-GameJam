using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movementManager : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;

    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;
    private Vector2 aimVector = Vector2.zero;
    
    public GameObject player;
    public GameObject target;

    private Rigidbody2D rb;
    
    public float speed = 5f;

    public bool usingController;
    public bool isMindControl;

    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Control.movement.performed += OnMovementPerformed;
        input.Control.movement.canceled += OnMovementCancelled;

        input.Control.mindControl.performed+=OnMindControllPerformed;
        
        input.Control.aim.performed += OnAimPerformed;
        input.Control.aim.canceled += OnAimCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.movement.performed -= OnMovementPerformed;
        input.Control.movement.canceled -= OnMovementCancelled;

        input.Control.mindControl.performed-=OnMindControllPerformed;

        input.Control.aim.performed -= OnAimPerformed;
        input.Control.aim.canceled -= OnAimCancelled;
    }
    private void OnMindControllPerformed(InputAction.CallbackContext context)
    {
        if(!isMindControl && fieldOfView.targetObject != null)
        {
                rb.velocity = new Vector2(0,0);
                target = fieldOfView.targetObject;
                isMindControl = true;
                
        }
        else
        {
                rb.velocity = new Vector2(0,0);
                target = player;
                isMindControl = false;
        }
        
    }
    private void OnAimPerformed(InputAction.CallbackContext value)
    {
        aimVector = value.ReadValue<Vector2>();
    }
    private void OnAimCancelled(InputAction.CallbackContext value)
    {
        aimVector = Vector2.zero;
    }


    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    void Start(){
        target = player;
    }

    void FixedUpdate()
    {
        rb = target.GetComponent<Rigidbody2D>();

        Debug.Log(fieldOfView.targetObject);
        
        rb.velocity = moveVector * speed;
        if (!usingController){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - rb.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
        }
        else {
            if(aimVector != Vector2.zero){
            float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
            }
            else if(moveVector != Vector2.zero){
            float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
            }
        }
    }
}
