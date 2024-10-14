using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]
    public int maxHealth = 3;
    public int damage;
    public float moveSpeed;
    public int moneyWorth = 10;


    public Transform playerPos;
    public Rigidbody2D rb;

    public StoreManager storeManager;

    public ParticleSystem impactParticles;
   
    
    private int health;

    void Start()
    {
        health = maxHealth;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        storeManager = GameObject.FindGameObjectWithTag("Store").GetComponent<StoreManager>();
    }

    private void Update()
    {
        EnemyAI();
        if (health <= 0)
        {
            // Death
            Destroy(gameObject);
            storeManager.money += moneyWorth;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health -= playerPos.gameObject.GetComponentInChildren<GunController>().curWeaponDamage;
        }
    }

    void TakeDamage()
    {

    }

    void EnemyAI()
    {
        if (transform.position.x > playerPos.position.x)
        {
            rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x < playerPos.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
