using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image HealthBar;
    public static event Action OnPlayerDeath;
    public float m_StartingHealth = 100f;
    public bool m_Dead;
    public float m_CurrentHealth;

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
        HealthBar.fillAmount = (m_CurrentHealth / m_StartingHealth);
        Debug.Log(m_CurrentHealth);
        if ((m_CurrentHealth <= 0f) && !(m_Dead))
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        m_Dead = true;
        OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
