using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Slime : BaseEnemy
{
    [SerializeField] private float attackRadius;

    [SerializeField] private bool shouldRotate;

    [SerializeField] private Transform target;
    NavMeshAgent agent;

    private Vector2 movement;
    [SerializeField] private Vector3 dir;

    private bool timerIsRunning;
    private float timeRemaining;
    [SerializeField] private float duration = 7f;
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

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, Player);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, Player);

        if (timerIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            } else {
                PlayerMovement.debuffMoveSpeed = 1f;
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
            if (coolDown <= canAttack) {
                // play attack animation here
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                canAttack = 0;
                PlayerMovement.debuffMoveSpeed = debuffScale;
                timerIsRunning = true;
            } else {
                canAttack += Time.deltaTime;
            }
        }
    }
}
