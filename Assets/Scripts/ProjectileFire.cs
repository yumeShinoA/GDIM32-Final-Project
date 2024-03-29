using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{

    [SerializeField] private float m_Damage = 20f;
    private float m_Bonus_Damage = 0f;
    [SerializeField] private float m_MaxLifeTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();
        /*if (targetRigidbody)
        {
            PlayerHealth playerHealth = targetRigidbody.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(m_Damage);
            }
        }
        */
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            enemy.TakeDamage(m_Damage+m_Bonus_Damage);
            Debug.Log(m_Damage + m_Bonus_Damage);
        }
        Destroy(gameObject);
    }
    public void IncreaseDamage(float damageIncrease)
    {
        m_Bonus_Damage += damageIncrease;
        gameObject.SetActive(true);
    }
}
