using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{   
    [SerializeField] protected Animator animator;
    [SerializeField] protected MonsterAI monsterAI;
    protected int defaultHP = 100;

    protected void Update()
    {
        OnDead();
    }

    public override void OnDead()
    {
        if (isDead == true)
        {
            // Set enemy layer to default
            transform.gameObject.layer = 0;
            // Despawn enemy
            StartCoroutine(DespawnAfterTime(3));
        }
    }

    public override void Respawn()
    {
        // Set enemy isDead boolean to false
        isDead = false;

        // Set enemy HP to default 
        hP = defaultHP;

        // Set enemy layer to Enemy
        transform.gameObject.layer = 7;

        // Enable enemy controller
        monsterAI.enabled = true;
        //transform.GetComponent<MonsterAI>().enabled = true;

        // Set enemy to chase player
        //transform.parent.GetComponent<EnemyController>().chasePlayer = true;
    }

    protected IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        EnemySpawner.Instance.Despawn(transform);
    }
}
