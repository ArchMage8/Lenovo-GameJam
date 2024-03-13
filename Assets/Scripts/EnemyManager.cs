using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool possessed = false;
    public bool chaseTarget = false;
    public Vector3 initialPosition;

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
       

      
    }
}
