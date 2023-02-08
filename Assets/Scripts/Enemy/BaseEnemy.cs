using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
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
    protected Animator anim;

    protected bool dead = false;
}
