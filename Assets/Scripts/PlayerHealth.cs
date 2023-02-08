using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    private bool m_Dead;
    private float m_CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;
        Debug.Log(m_CurrentHealth);
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        m_Dead = true;
        // other dying actions here
    }
}
