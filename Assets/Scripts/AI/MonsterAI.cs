using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Transform player;
    [SerializeField] protected Animator animator;

    [SerializeField] protected float attackDistance;
    protected float DistanceToPlayer => (transform.position - player.position).magnitude;

    protected void Update()
    {
        Chase();
    }

    protected void Chase()
    {
        if (DistanceToPlayer > 10 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Sit_Attack_2"))
        {
            agent.destination = player.position;
            animator.SetBool("Chase", true);
        }
        else if (DistanceToPlayer <= attackDistance)
        {
            Vector3 lookAtPosition = player.position;
            lookAtPosition.y = transform.position.y;
            transform.LookAt(lookAtPosition);

            animator.SetBool("Chase", false);
            animator.SetTrigger("Attack");
        }
    }
}