using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField]private Transform direction_transform;
    private Rigidbody2D playerBody;
    private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    private void Start()
    {
       
        agent = GetComponentInParent<NavMeshAgent>();
        playerBody = GetComponentInParent<Rigidbody2D>();
       

        if(direction_transform == null && agent == null)
        {
            Debug.LogError("Missing Components");
        }
    }

    void Update()
    {
        if (animator != null)
        {
            if (agent != null) //Untuk AI Enemy
            {

                if (agent.velocity.magnitude >= 0.1 || playerBody.velocity.magnitude >= 0.1)
                {
                    //Debug.Log("Movement1");
                    animator.SetBool("IsMoving", true);
                    MovingdirectionCheck();
                }

                else if (agent.velocity.magnitude < 0.1 || playerBody.velocity.magnitude < 0.15)
                {
                    //Debug.Log("Movement2");
                    animator.SetBool("IsMoving", false);
                    IdleDirectionCheck();
                }
            }

            else if (agent == null) //Untuk Player
            {
                if (playerBody.velocity.magnitude >= 0.1)
                {
                    //Debug.Log("Movement3");
                    animator.SetBool("IsMoving", true);
                    MovingdirectionCheck();
                }

                else
                {
                    //Debug.Log("Movement4");
                    animator.SetBool("IsMoving", false);
                    IdleDirectionCheck();
                }
            }
        }
    }

    private void MovingdirectionCheck()
    {
        if (CheckZRotationRange(transform, 0f, 45f) || CheckZRotationRange(transform, -45f, 0f))
        {

            //Debug.Log("MovingUp");
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Horizontal", 0);
        }

        if (CheckZRotationRange(transform, 46f, 135f))
        {
            //Debug.Log("MovingLeft");
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);
        }

        if (CheckZRotationRange(transform, -135f, -46f))
        {
            //Debug.Log("MovingRight");
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);
        }

        if (CheckZRotationRange(transform, -180f, -136f) || CheckZRotationRange(transform, 136f, 180f))
        {
            //Debug.Log("MovingDown");
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Horizontal", 0);
        }
    }
    
    private void IdleDirectionCheck()
    {
        if (CheckZRotationRange(transform, 0f, 45f) || CheckZRotationRange(transform, -45f, 0f))
        {
            //Debug.Log("IdleUp");
            animator.SetFloat("IdleVertical", 1);
            animator.SetFloat("IdleHorizontal", 0);
        }

        if (CheckZRotationRange(transform, 46f, 135f))
        {
            //Debug.Log("IdleLeft");
            animator.SetFloat("IdleHorizontal", -1);
            animator.SetFloat("IdleVertical", 0);

        }

        if (CheckZRotationRange(transform, -135f, -46f))
        {
            //Debug.Log("IdleRight");
            animator.SetFloat("IdleHorizontal", 1);
            animator.SetFloat("IdleVertical", 0);

        }

        if (CheckZRotationRange(transform, -180f, -136f) || CheckZRotationRange(transform, 136f, 180f))
        {
            //Debug.Log("IdleDown");
            animator.SetFloat("IdleVertical", -1);
            animator.SetFloat("IdleHorizontal", 0);
        }
    }

    public bool CheckZRotationRange(Transform transform, float rangeStart, float rangeEnd)
    {
        float zRotation = direction_transform.rotation.eulerAngles.z;

        if (zRotation > 180f)
        {
            zRotation -= 360f;
        }

        Debug.Log(zRotation);

        return zRotation >= rangeStart && zRotation <= rangeEnd;
    }
}
