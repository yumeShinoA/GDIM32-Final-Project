using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float checkRadius;
    [SerializeField] private float attackRadius;

    [SerializeField] private bool shouldRotate;

    [SerializeField] private Transform target;
    NavMeshAgent agent;

    private Vector2 movement;
    [SerializeField] private Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    private float canAttack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    private void Update()
    {
        //anim.SetBool("Walk", isInChaseRange);      Animation needs fixing

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, Player);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, Player);

        if (isInChaseRange) {
            agent.SetDestination(target.position);
        }

    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player") {
            if(coolDown <= canAttack) {
                //other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                canAttack = 0;
            } else {
                canAttack += Time.deltaTime;
            }
        }
    }
}
