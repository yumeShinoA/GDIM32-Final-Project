using System.Collections;
using UnityEngine;

public class FireballEnemy : BaseEnemy {
    
    [SerializeField] private float throwSpeed = 10f; // Speed at which the enemy throws the fireball
    [SerializeField] private GameObject fireballPrefab; // Prefab of the fireball object

    private bool isCharging = false; // Whether the enemy is currently charging the fireball

    private float currentSize;
    private Vector3 originalScale;
    [SerializeField] private float maxChargeSize;

    [SerializeField] private Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        originalScale = fireballPrefab.transform.localScale;
        currentSize = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        // If the player enters attack range
        if (target != null && distanceToPlayer < checkRadius && !isCharging)
        {
            isCharging = true;

            GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);
            StartCoroutine(ChargeFireball(fireball));
        }
    }

    IEnumerator ChargeFireball(GameObject fireball)
    {    
        // Increase the fireball size over time while charging
        while (currentSize < maxChargeSize)
        {
            currentSize += Time.deltaTime * maxChargeSize;
            fireball.transform.localScale = originalScale * currentSize;
            yield return null;
        }

        // Resets
        isCharging = false;
        currentSize = 0f;

        LaunchFireball(fireball);

    }

    // Function for launching the fireball
    void LaunchFireball(GameObject fireball)
    {
        // Calculate the direction to launch the fireball
        Vector2 direction = (target.position - transform.position).normalized;

        // Launch the fireball in the calculated direction
        fireball.GetComponent<Rigidbody2D>().velocity = direction * throwSpeed;
    }
}

