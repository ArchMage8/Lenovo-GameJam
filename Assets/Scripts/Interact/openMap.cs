using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class openMap : MonoBehaviour
{
    private PlayerControls input = null;
    [HideInInspector] public bool mapOpen = false;
    [Header("Drag Map Game Object Here")]
    public GameObject map;
    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Control.map.performed += OnMapPerformed;
        input.Control.map.canceled += OnMapCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.map.performed -= OnMapPerformed;
        input.Control.map.canceled -= OnMapCancelled;
    }
    private void OnMapPerformed(InputAction.CallbackContext context)
    {
        mapOpen = true;
        map.SetActive(true);
    }
    private void OnMapCancelled(InputAction.CallbackContext context)
    {
        if(mapOpen){   
            mapOpen = false;
            map.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
