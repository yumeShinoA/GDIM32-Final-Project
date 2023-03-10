using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : BaseEnemy
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

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, Player);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, Player);

        // Enable collider when player is in attack range, and change opacity
        if (isInAttackRange) {
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        } else {
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        }
    }

    private void FixedUpdate()
    {
        if (isInChaseRange) {
            agent.SetDestination(target.position);
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
    */

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {
            if (coolDown <= canAttack) { // Attack Player
                // play attack animation here
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                canAttack = 0;
            } else {
                canAttack += Time.deltaTime; // Enemy wait for cooldown
            }
        }
    }
}
