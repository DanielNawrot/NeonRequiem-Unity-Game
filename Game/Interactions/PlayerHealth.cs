using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;
    public LayerMask enemyLayer;
    public HealthBar healthBar;

    public LevelLoader levelLoader;


    public float knockbackForce = 20f;

    private void Update()
    {
        if (currentHealth <= 0)
        {
            levelLoader.LoadNextLevel(0);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            currentHealth--;
        }
    }
}
