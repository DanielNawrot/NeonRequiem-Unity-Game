using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float lifeTime = 5f;

    public bool destroyOnContact = true;

    public Animator animator;
    Rigidbody2D rb;

    public ParticleSystem impactParticles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 6)
        {
            var em = impactParticles.emission;
            var dur = impactParticles.main.duration;
            em.enabled = true;
            impactParticles.Play();
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(rb);
            Destroy(gameObject.GetComponent<Collider2D>());
            Destroy(gameObject, dur);
        }
    }
}
