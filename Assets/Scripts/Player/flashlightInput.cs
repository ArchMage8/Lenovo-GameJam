using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class flashlightInput : MonoBehaviour
{
    [Header("Drag FOV Prefab Inside the Player Prefab Here")]
    [SerializeField] PlayerFOV fieldOfView;
    public float lightIntensityOn;
    public float lightIntensityOff;

    private EnemyManager enemymanager;

    
    [HideInInspector] public GameObject player;

    [HideInInspector] public GameObject flashlightobject;

    [HideInInspector] public GameObject flashlightcircleobject;

    private PlayerControls input = null;

    [HideInInspector] public bool flashlight;

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
    void Start()
    {
        player = GameObject.Find("Player");
        flashlightobject = GameObject.Find("PlayerFlashlight");
        flashlightcircleobject = GameObject.Find("PlayerCircleLight");
    }
    void Update()
    {
        if(flashlight){
            flashlightobject.GetComponent<Light2D>().intensity = lightIntensityOn;
            flashlightcircleobject.GetComponent<Light2D>().intensity = lightIntensityOn;
        }
        else{
            flashlightobject.GetComponent<Light2D>().intensity = lightIntensityOff;
            
        }
        if(flashlight && fieldOfView.targetObject != null){
            fieldOfView.targetObject.GetComponent< EnemyMovement>().Transform_Movement(player.GetComponent<Transform>());
            fieldOfView.targetObject.GetComponent<EnemyManager>().isPatrolling = false;
        }
    }
}
