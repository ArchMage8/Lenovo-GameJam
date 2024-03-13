using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool possessed = false;
    public bool chasePlayer = false;
    private Vector3 initialPosition;

    private SuspicionDetection suspicionDetection;
    private DirectDetection directDetection;
    

    private void Awake()
    {
        initialPosition = transform.position;
        suspicionDetection = GetComponentInChildren<SuspicionDetection>();
        directDetection = GetComponentInChildren<DirectDetection>();
    }

    private void Update()
    {
        if (chasePlayer)
        {
            suspicionDetection.enabled = false; 
            directDetection.enabled = false;
        }
        else
        {
            suspicionDetection.enabled = true;
            directDetection.enabled = true;
        }
    }
}
