using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class flashlightInput : MonoBehaviour
{
    [SerializeField] DetectionSystem DetectionSystem;
    [SerializeField] PlayerFOV fieldOfView;

    private EnemyManager enemymanager;

    public GameObject flashlightobject;

    private PlayerControls input = null;

    public bool flashlight;

    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Control.flashlightControl.performed += OnflashlightPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.flashlightControl.performed -= OnflashlightPerformed;
    }
     private void OnflashlightPerformed(InputAction.CallbackContext context)
    {
        flashlight = !flashlight;
    }

    void Update()
    {
        if(flashlight){
            flashlightobject.SetActive(true);
        }
        else{
            flashlightobject.SetActive(false);
        }
        if(flashlight && fieldOfView.targetObject != null){
            fieldOfView.targetObject.GetComponent< EnemyMovement>().Transform_Movement(transform);
            fieldOfView.targetObject.GetComponent<EnemyManager>().isChasing = true;
            // fieldOfView.targetObject.GetComponent<DetectionSystem>().chasingPlayer();
        }
    }
}
