using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    //this class is responsible for activiating the correct function to boost the
    //corresponsing player stat
    [SerializeField]
    private GameObject Player;

    private PlayerHealth playerhp;

    private PlayerMovement playermovement;

    private PlayerShooting playershooting;

    [SerializeField]
    private GameObject bullet;

    private ProjectileFire projectile;

    [SerializeField]
    private float hpbuff = 30f;

    [SerializeField]
    private float speedbuff = 1f;

    [SerializeField]
    private float DamageBuff = 10f;

    [SerializeField]
    private float BulletBuff = 5f;

    [SerializeField]
    private float dashBuff = 5f;

    void Start()
    {
        playerhp = Player.GetComponent<PlayerHealth>();
        projectile = bullet.GetComponent<ProjectileFire>();
        playermovement = Player.GetComponent<PlayerMovement>();
        playershooting = Player.GetComponent<PlayerShooting>();
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

    public void HigherDamage()
    {
        projectile.IncreaseDamage(DamageBuff);
        Player.SetActive(true);
    }

    public void BulletTravelFaster()
    {
        playershooting.ProjectileBuff(BulletBuff);
        Player.SetActive(true);

    }

    public void dashRangeBuff()
    {
        playermovement.dashRangeBuff(dashBuff);

    }
}
