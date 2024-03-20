using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movementManager : MonoBehaviour
{
    [SerializeField] openMap openMap;
    private PlayerControls input = null;
    private Vector2 moveVector = Vector2.zero;
    private Vector2 aimVector = Vector2.zero;
    
    public GameObject player;
    public GameObject target;

    public Rigidbody2D rb;
    
    public float speed = 5f;

    public bool usingController;

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

        input.Control.aim.performed += OnAimPerformed;
        input.Control.aim.canceled += OnAimCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.movement.performed -= OnMovementPerformed;
        input.Control.movement.canceled -= OnMovementCancelled;

        input.Control.aim.performed -= OnAimPerformed;
        input.Control.aim.canceled -= OnAimCancelled;
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
        if(!openMap.mapOpen){
            rb = target.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 0;
            rb.velocity = moveVector * speed;
        }
        else{
            rb.velocity = new Vector2(0,0);
            rb.angularVelocity = 0;
        }

        if (!usingController){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
