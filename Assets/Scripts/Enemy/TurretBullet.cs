using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public GameObject explosionFX;
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
    private float timer;
    public int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dir = player.transform.position - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;
        float rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot+180);
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Tank_Health health = col.gameObject.GetComponent<Tank_Health>();
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
