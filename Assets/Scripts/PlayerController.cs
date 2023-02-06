using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float Speed = 5.0f;

    private Rigidbody2D m_playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody2D>();
        if (m_playerRigidbody == null) {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 UserInput = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.Translate(UserInput * Speed * Time.deltaTime);

    }

}
