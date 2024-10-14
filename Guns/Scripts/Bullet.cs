using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float lifeTime = 5f;

    public LayerMask terrain;
    public LayerMask enemy;

    public bool destroyOnContact = true;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnContact)
        {
            if (collision.gameObject.layer == 6 || collision.gameObject.layer == 8)
            {
                Destroy(gameObject);
            }
        } 
    }
}
