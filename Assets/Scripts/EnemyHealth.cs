using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float current_hp;
    public float max_hp = 3f;


    // Start is called before the first frame update
    void Start()
    {
        current_hp = max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage( float damage)
    {
        current_hp -= damage;
        if (current_hp<= 0)
        {
            Destroy(gameObject);
        }
    }
}
