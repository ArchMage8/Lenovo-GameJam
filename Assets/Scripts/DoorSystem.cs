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
    public bool interacting;

    [SerializeField] private GameObject TargetDoor;
    [SerializeField] private GameObject ClosedVisual;
    [SerializeField] private GameObject OpenedVisual;

    private Collider2D DoorCollider;
    private NavMeshModifier Wall;

    private void Start()
    {
        ClosedVisual.SetActive(true);
        OpenedVisual.SetActive(false);
        DoorCollider = TargetDoor.GetComponent<Collider2D>();
        Wall = TargetDoor.GetComponent<NavMeshModifier>();
    }
    private void OnCollisionEnter2D(Collision2D Collider)
    {
        
            if(interacting)
            {
                ClosedVisual.SetActive(false);
                OpenedVisual.SetActive(true);
                Wall.overrideArea = false;
               
            }
        
    }
}
