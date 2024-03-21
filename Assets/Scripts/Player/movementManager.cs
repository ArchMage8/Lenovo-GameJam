using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movementManager : MonoBehaviour
{
    [HideInInspector] public openMap openMap;
    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;
    private Vector2 aimVector = Vector2.zero;
    
    [HideInInspector] public GameObject target;
    [HideInInspector] public GameObject player;

    private Vector2 mousePosition;
    private Vector2 lastMousePosition;
    [HideInInspector] public Rigidbody2D rb;
    [Header("Adjust Player Speed")]
    public float speed = 5f;

    [HideInInspector] public bool usingController;

    private EnemyManager enemyManager;

    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Control.movement.performed += OnMovementPerformed;
        input.Control.movement.canceled += OnMovementCancelled;

        input.Control.movementkbm.performed += OnMovementkbmPerformed;
        input.Control.movementkbm.canceled += OnMovementkbmCancelled;

        input.Control.aim.performed += OnAimPerformed;
        input.Control.aim.canceled += OnAimCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.movement.performed -= OnMovementPerformed;
        input.Control.movement.canceled -= OnMovementCancelled;

        input.Control.movementkbm.performed += OnMovementkbmPerformed;
        input.Control.movementkbm.canceled += OnMovementkbmCancelled;

        input.Control.aim.performed -= OnAimPerformed;
        input.Control.aim.canceled -= OnAimCancelled;
    }
    private void OnAimPerformed(InputAction.CallbackContext value)
    {
        usingController = true;
        aimVector = value.ReadValue<Vector2>();
    }
    private void OnAimCancelled(InputAction.CallbackContext value)
    {
        aimVector = Vector2.zero;
    }


    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        usingController = true;
        moveVector = value.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        
        moveVector = Vector2.zero;
    }

    private void OnMovementkbmPerformed(InputAction.CallbackContext value)
    {
        usingController = false;
        moveVector = value.ReadValue<Vector2>();
    }
    private void OnMovementkbmCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }

    void Start(){
        player = GameObject.FindWithTag("Player");
        target = player;
        openMap = GetComponent<openMap>();
    }

    void FixedUpdate()
    {
        mousePosition = Input.mousePosition;
        if (mousePosition != lastMousePosition)
        {
            usingController = false;
        }
        lastMousePosition = mousePosition;

        if(!openMap.mapOpen){
            rb = target.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 0;
            rb.velocity = moveVector * speed;
        }
        else{
            rb.velocity = new Vector2(0,0);
            rb.angularVelocity = 0;
        }
    
        if (!usingController && !openMap.mapOpen){
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - rb.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
        }
        else if(!openMap.mapOpen){
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
