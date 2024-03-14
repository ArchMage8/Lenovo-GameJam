using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool isPossessed;
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }
}
