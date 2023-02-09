using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    protected PlayerHealth PlayerHealth;
    protected Animator anim;

    [SerializeField]
    protected float checkRadius;
}
