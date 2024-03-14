using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField] movementManager movementManager;
    public Transform target;
    public float smoothSpeed = 0.1f;

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(movementManager.target.transform.position.x, movementManager.target.transform.position.y, -100f);
    
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
