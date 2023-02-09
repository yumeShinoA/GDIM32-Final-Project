using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Slime : BaseEnemy
{
    [SerializeField] private float attackRadius;

    [SerializeField] private bool shouldRotate; // Unused

    [SerializeField] private Transform target;
    NavMeshAgent agent;

    private Vector2 movement; // Unused
    [SerializeField] private Vector3 dir; // Unused

    private bool timerIsRunning;
    private float timeRemaining; // debuff timer
    [SerializeField] private float duration = 7f; // debuff time duration
    [SerializeField] private float debuffScale;

    private bool isInChaseRange;
    private bool isInAttackRange;

    private float canAttack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.isKinematic = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        timeRemaining = duration;

    }

    private void Update()
    {
        //anim.SetBool("Run", isInChaseRange);  Needs to link animation

        // Check for player
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, Player);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, Player);

        // Debuff timer countdown
        if (timerIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            } else {
                PlayerMovement.debuffMoveSpeed = 1f; // return player speed back to original
                timeRemaining = duration;
                timerIsRunning = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if (isInChaseRange) {
            agent.SetDestination(target.position);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") {
            if (coolDown <= canAttack) { // attack player
                // play attack animation here
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                canAttack = 0;
                PlayerMovement.debuffMoveSpeed = debuffScale; // change player speed based on debuffScale
                timerIsRunning = true;
            } else { // enemy attack cooldown
                canAttack += Time.deltaTime;
            }
        }
    }
}
