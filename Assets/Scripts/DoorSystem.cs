using NavMeshPlus.Components;
using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorSystem : MonoBehaviour
{
    public enum TargetTags
    {
        Janitor,
        Guard,
        Scientist,
        IT,
        All
    }

    public TargetTags canInteract;
    private bool interacting;
  
    
    [SerializeField] private GameObject ClosedVisual;
    [SerializeField] private GameObject OpenedVisual;

    private Collider2D TriggerCollider;
    private NavMeshModifier Wall;

    private void Start()
    {
        ClosedVisual.SetActive(true);
        OpenedVisual.SetActive(false);
        TriggerCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(interacting && Input.GetKeyDown(KeyCode.P))
        {
            ClosedVisual.SetActive(false);
            OpenedVisual.SetActive(true);

            TriggerCollider.enabled = false;
        }
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
