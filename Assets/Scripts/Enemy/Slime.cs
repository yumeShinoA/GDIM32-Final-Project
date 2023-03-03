using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Slime : BaseEnemy
{
    private bool timerIsRunning;
    private float timeRemaining; // debuff timer
    [SerializeField] private float duration = 7f; // debuff time duration
    [SerializeField] private float debuffScale;

    [SerializeField] private GameObject PlayerChar;
    static public GameObject childObject;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.isKinematic = true;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        timeRemaining = duration;

        childObject = PlayerChar.transform.GetChild(0).gameObject;

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
                // Change player color to original
                childObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
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
                childObject.GetComponent<SpriteRenderer>().color = new Color(143f, 0f, 254f); // change player color to purple
                timerIsRunning = true;
            } else { // enemy attack cooldown
                canAttack += Time.deltaTime;
            }
        }
    }

    // temporary, will move to player stat script
    static public void diableDebuff(GameObject child)
    {
        PlayerMovement.debuffMoveSpeed = 1f;
        child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }
}
