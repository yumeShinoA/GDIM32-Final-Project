using UnityEngine;

public class Fireball : MonoBehaviour {
    
    [SerializeField] private float damage = 10f; // Amount of damage the fireball will cause
    [SerializeField] private float m_MaxLifeTime = 2f;

    void Start()
    {
        //Destroy(gameObject, m_MaxLifeTime);
    }

    // Called when the fireball collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D targetRigidbody = collision.GetComponent<Rigidbody2D>();
        if (targetRigidbody.CompareTag("Player"))
        {
            PlayerHealth playerHealth = targetRigidbody.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}