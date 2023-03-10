using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : BaseEnemy
{

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.isKinematic = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    private void Update()
    {
        //anim.SetBool("Run", isInChaseRange);  Needs to link animation

        // Check if player is in range
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, Player);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, Player);

    }

    private void FixedUpdate()
    {
        if (isInChaseRange) {
            agent.SetDestination(target.position);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player") {
            if(coolDown <= canAttack) { // Attack player
                // play attack animation here
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                canAttack = 0;
            } else {
                canAttack += Time.deltaTime; // enemy wait for cooldown
            }
        }
    }
}
