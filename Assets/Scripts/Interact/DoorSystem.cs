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
        IT,
        Player
    }

    public TargetTags canInteract;
    
  
    
    [SerializeField] private GameObject ClosedVisual;
    [SerializeField] private GameObject OpenedVisual;
    [SerializeField] private GameObject InteractText;

    private Collider2D TriggerCollider;
    private NavMeshObstacle doorMesh;
    private bool interacting;

    private PlayerControls input = null;

    [Header("Audio")]
    [SerializeField] private bool remote;// unutk ganti suara
    public AudioClip RemoteOpen;
    public float RemoteVolume;
    public AudioClip DirectOpen;
    public float DirectVolume;

    private void Awake() 
    {
        input = new PlayerControls();
    }

    private void OnEnable() 
    {
        input.Enable();
        input.Control.interact.performed += OnInteractPerform;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Control.interact.performed -= OnInteractPerform;
    }
     private void OnInteractPerform(InputAction.CallbackContext context)
    {
        if (interacting)
        {
            ClosedVisual.SetActive(false);
            OpenedVisual.SetActive(true);

            TriggerCollider.enabled = false;
            doorMesh.enabled = false;

            if (remote)
            {
                SoundManager.instance.PlaySound(RemoteOpen, RemoteVolume);
            }

            else if (!remote)
            {
                SoundManager.instance.PlaySound(DirectOpen, DirectVolume);
            }
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
        Debug.Log(collision.name);
        if (collision.tag == canInteract.ToString() && collision.GetComponent<EnemyManager>().isPossessed)
        {
            Debug.Log("it work");
            interacting = true;
            InteractText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == canInteract.ToString() && collision.GetComponent<EnemyManager>().isPossessed)
        {
            interacting = false;
            InteractText.SetActive(false);
        }
    }


   
}
