using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected EnemyHealth enemyHealth;
    [SerializeField] protected PlayerHealth playerHealth => GameObject.Find("Player").GetComponent<PlayerHealth>();
    [SerializeField] protected Transform player => GameObject.Find("Player").transform;

    protected float DistanceToPlayer => (transform.position - player.position).magnitude;

    protected void Update()
    {
        Chase();
        CheckDeath();
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
            Attack();
        }
    }

    protected void Attack()
    {
        Vector3 lookAtPosition = player.position;
        lookAtPosition.y = transform.position.y;
        transform.LookAt(lookAtPosition);

        animator.SetBool("Chase", false);
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackState", Random.Range(0, 6));
    }

    protected void CheckDeath()
    { 
        if (enemyHealth.isDead)
        {
            animator.SetTrigger("Death");
            animator.SetBool("Chase", false);
            this.enabled = false;
            agent.isStopped = true;
        }
    }

    protected void SendDamage()
    {
        if (DistanceToPlayer <= attackDistance)
        {
            playerHealth.TakeDamage(10);
        }
    }
}