using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Enemy[] enemies;
    public int currentEnemyIndex = 0;
    private Enemy currentEnemy;

    public LayerMask playerBullet;

    public int maxHealth = 3;
    public int health;
    

    void Start()
    {
        currentEnemy = enemies[currentEnemyIndex];
        maxHealth = currentEnemy.MaxHealth;
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            health--;
        }
    }
}
