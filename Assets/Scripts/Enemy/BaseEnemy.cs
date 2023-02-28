using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour {
    [SerializeField]
    protected float health = 100f;
    [SerializeField]
    protected float damage = 10f;
    [SerializeField]
    protected float speed = 10f;
    [SerializeField]
    protected float coolDown = 1f;

    [SerializeField] protected LayerMask Player;
    protected Rigidbody2D rb;

    [SerializeField] protected float attackRadius;

    [SerializeField] protected Transform target;
    protected NavMeshAgent agent;

    protected PlayerHealth PlayerHealth;
    protected Animator anim;

    [SerializeField]
    protected float checkRadius;

    protected bool isInChaseRange;
    protected bool isInAttackRange;

    protected float canAttack;
}
