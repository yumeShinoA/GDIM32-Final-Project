using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{

    public float m_Damage = 20f;
    public float m_MaxLifeTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();
        if(targetRigidbody)
        {
            PlayerHealth playerHealth = targetRigidbody.GetComponent<PlayerHealth>();
            if(playerHealth)
            {
                playerHealth.TakeDamage(m_Damage);
            }
        }
        Destroy(gameObject);
    }
}
