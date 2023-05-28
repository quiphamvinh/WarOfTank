using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionFX;

    public float speed =10f;
    public int damage;
    public float lifetime = 2f;

    private void Start()
    {
        Destroy(this.gameObject, lifetime);
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Enemy_Health health = collision.gameObject.GetComponent<Enemy_Health>();
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

        if (collision.collider.CompareTag("Boss"))
        {
            Boss_Health health = collision.gameObject.GetComponent<Boss_Health>();
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

