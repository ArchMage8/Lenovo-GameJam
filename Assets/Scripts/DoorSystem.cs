using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private GameObject ClosedVisual;
    [SerializeField] private GameObject OpenedVisual;

    private Collider2D component;

    private void Start()
    {
        ClosedVisual.SetActive(true);
        OpenedVisual.SetActive(false);

        component = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canInteract == TargetTags.All || collision.CompareTag(canInteract.ToString()))
        {
            if(interacting)
            {
                ClosedVisual.SetActive(false);
                OpenedVisual.SetActive(true);

                component.enabled = false;
            }
        }
    }
}
