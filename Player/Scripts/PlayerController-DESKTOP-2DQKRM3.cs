using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 3;
    public int playerHealth;

    private BoxCollider2D bc;
    public LayerMask enemyBullet;
    public float knockbackForce = 20f;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        playerHealth = maxHealth;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == enemyBullet)
        {
            maxHealth--;
            Vector2 difference = (transform.position - other.transform.position).normalized;
            Vector2 force = difference * knockbackForce;
            gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }
}
