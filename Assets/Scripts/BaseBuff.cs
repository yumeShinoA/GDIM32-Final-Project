using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    private PlayerHealth playerhp;

    private PlayerMovement playermovement;

    [SerializeField]
    private GameObject bullet;

    private ProjectileFire projectile;

    [SerializeField]
    private float hpbuff = 30f;

    [SerializeField]
    private float speedbuff = 1f;

    [SerializeField]
    private float DamageBuff = 10f;

    void Start()
    {
        playerhp = Player.GetComponent<PlayerHealth>();
        projectile = bullet.GetComponent<ProjectileFire>();
        playermovement = Player.GetComponent<PlayerMovement>();
        Player.SetActive(false);
    }
    public void addHealth()
    {
        playerhp.addHp(hpbuff);
    }

    public void Agility()
    {
        playermovement.IncreaseSpeed(speedbuff);
    }

    public void Harder()
    {
        projectile.IncreaseDamage(DamageBuff);
        Player.SetActive(true);
    }

}
