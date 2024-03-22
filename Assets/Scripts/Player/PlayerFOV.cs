using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    public float radius = 5f;

    [Range(1, 360)]
    public float angle = 45f;

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstructionLayer;

    public GameObject targetObject;
    private GameObject ParentObject;


    public bool CanSeeTarget { get; private set; }

    private void Start()
    {
        StartCoroutine(FOVCheck());

    }



    private IEnumerator FOVCheck()
    {
        new WaitForSeconds(0f);

        while (true)
        {
            yield return null;
            FOV();
        }
    }

    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);


        if (rangeCheck.Length > 0)
        {
            foreach (Collider2D col in rangeCheck)
            {
                Vector2 directionToTarget = (col.transform.position - transform.position).normalized;

                if (Vector2.Angle(transform.up, directionToTarget) < angle / 2f)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, col.transform.position);

                    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                    {
                        CanSeeTarget = true;
                        targetObject = col.gameObject;
                        break;
                    }
                    else
                    {
                        CanSeeTarget = false;
                        targetObject = null;
                    }
                }
                else
                {
                    CanSeeTarget = false;
                    targetObject = null;
                }
            }
        }
        else
        {
            CanSeeTarget = false;
            targetObject = null;
        }
    }




   

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
