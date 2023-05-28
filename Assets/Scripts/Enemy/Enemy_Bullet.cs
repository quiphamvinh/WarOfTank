using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public GameObject explosionFX;

    public float speed = 20f;
    public int damage = 2;
    public float lifetime = 3f;


    private void Start()
    {
        Destroy(this.gameObject, lifetime);
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Tank_Health health = collision.gameObject.GetComponent<Tank_Health>();
            health.TakeDamage(damage);
            GameObject explosion = Instantiate(explosionFX, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(explosion, 0.2f);
        }
        else
        {
            GameObject explosion = Instantiate(explosionFX, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.2f);
            Destroy(explosion, 0.2f);
        }
    }
}
