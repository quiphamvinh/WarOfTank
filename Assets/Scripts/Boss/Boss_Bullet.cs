using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    public GameObject explosionFX;
    private Vector2 moveDirection;
    public float damage = 0.5f;
    public float moveSpeed;
    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }
    private void Start()
    {
        moveSpeed = 8f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Tank_Health health = collision.gameObject.GetComponent<Tank_Health>();
            health.TakeDamage(damage);
            GameObject explosion = Instantiate(explosionFX, this.transform.position, Quaternion.identity);
            Destroy(explosion, 0.2f);
        }
        else
        {
            GameObject explosion = Instantiate(explosionFX, this.transform.position, Quaternion.identity);
            Destroy(explosion, 0.2f);
        }
    }
    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}