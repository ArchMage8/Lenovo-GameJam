using NavMeshPlus.Components;
using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;


public class DoorSystem : MonoBehaviour
{
    public enum TargetTags
    {
        Janitor,
        Guard,
        Scientist,
        IT
    }

    public TargetTags canInteract;
    private bool interacting;
  
    
    [SerializeField] private GameObject ClosedVisual;
    [SerializeField] private GameObject OpenedVisual;
    [SerializeField] private GameObject InteractText;

    private Collider2D TriggerCollider;
    private NavMeshObstacle doorMesh;

    private PlayerControls input = null;
    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Control.interact.performed += OnflashlightPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.interact.performed -= OnflashlightPerformed;
    }
     private void OnflashlightPerformed(InputAction.CallbackContext context)
    {
        if(interacting)
        {
            ClosedVisual.SetActive(false);
            OpenedVisual.SetActive(true);

            TriggerCollider.enabled = false;
            doorMesh.enabled = false;
        }
    }
    private void Start()
    {
        ClosedVisual.SetActive(true);
        OpenedVisual.SetActive(false);
        TriggerCollider = GetComponent<BoxCollider2D>();
        doorMesh = GetComponentInParent<NavMeshObstacle>();
        InteractText.SetActive(false) ;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == canInteract.ToString())
        {
            interacting = true;
            InteractText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == canInteract.ToString())
        {
            interacting = false;
            InteractText.SetActive(false);
        }
    }


   
}
