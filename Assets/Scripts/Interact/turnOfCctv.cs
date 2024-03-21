using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameObjectArrayExample : MonoBehaviour
{
    public enum TargetTags
    {
        Janitor,
        Guard,
        Scientist,
        IT
    }
    public TargetTags canInteract;

    [SerializeField]
    private GameObject[] targetGameObjects;
    
    private PlayerControls input = null;

    private bool interacting;

    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Control.interact.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.interact.performed -= OnInteractPerformed;
    }
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {   
        if(interacting){
            foreach (GameObject obj in targetGameObjects)
            {
                if (obj != null)
                {
                    obj.GetComponent<cctvMovement>().cctvOn = false;
                }
            }
        }
    }

    void Start()
    {
        

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == canInteract.ToString())
        {
            interacting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == canInteract.ToString())
        {
            interacting = false;
        }
    }
}