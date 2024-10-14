using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 3;
    public int playerHealth;

    private BoxCollider2D bc;
    public LayerMask enemyBullet;
    public 

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        playerHealth = maxHealth;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == enemyBullet)
        {
            maxHealth--;
        }
    }
}
