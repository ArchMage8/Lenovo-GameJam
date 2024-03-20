using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimationController : MonoBehaviour
{
    private Transform direction_transform;
    private Rigidbody2D playerBody;
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        direction_transform = GetComponentInParent<Transform>();
        agent = GetComponentInParent<NavMeshAgent>();
        playerBody = GetComponentInParent<Rigidbody2D>();

        if(direction_transform == null && agent == null)
        {
            Debug.LogError("Missing Components");
        }
    }

    void Update()
    {
     
        if(agent != null) //Untuk AI Enemy
        {
            if(agent.velocity.magnitude >= 0.1)
            {
                MovingdirectionCheck();
            }

            else
            {
                IdledirectionCheck();
            }
        }  

      else if(direction_transform != null && agent == null) //Untuk Player
        {
            if(playerBody.velocity.magnitude >= 0.1)
            {
                MovingdirectionCheck();
            }

            else
            {
                IdledirectionCheck();
            }
        }
    }

    private void MovingdirectionCheck()
    {
        if (CheckZRotationRange(transform, 0f, 45f) || CheckZRotationRange(transform, -45f, 0f))
        {
            Debug.Log("Up");
            animator.SetTrigger("Moving_Up");
        }

        if (CheckZRotationRange(transform, 46f, 135f))
        {
            Debug.Log("Left");
            animator.SetTrigger("Moving_Left");
        }

        if (CheckZRotationRange(transform, -135f, -46f))
        {
            Debug.Log("Right");
            animator.SetTrigger("Moving_Right");
        }

        if (CheckZRotationRange(transform, -180f, -136f) || CheckZRotationRange(transform, 136f, 180f))
        {
            Debug.Log("Down");
            animator.SetTrigger("Moving_Down");
        }
    }
    
    private void IdledirectionCheck()
    {
        if (CheckZRotationRange(transform, 0f, 45f) || CheckZRotationRange(transform, -45f, 0f))
        {
            Debug.Log("Up");
            animator.SetTrigger("Idle_Up");
        }

        if (CheckZRotationRange(transform, 46f, 135f))
        {
            Debug.Log("Left");
            animator.SetTrigger("Idle_Left");
        }

        if (CheckZRotationRange(transform, -135f, -46f))
        {
            Debug.Log("Right");
            animator.SetTrigger("Idle_Right");
        }

        if (CheckZRotationRange(transform, -180f, -136f) || CheckZRotationRange(transform, 136f, 180f))
        {
            Debug.Log("Down");
            animator.SetTrigger("Idle_Down");
        }
    }

    public bool CheckZRotationRange(Transform transform, float rangeStart, float rangeEnd)
    {
        float zRotation = transform.eulerAngles.z;

        if (zRotation > 180f)
        {
            zRotation -= 360f;
        }

        return zRotation >= rangeStart && zRotation <= rangeEnd;
    }
}
