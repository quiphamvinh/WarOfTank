using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireofGas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Tank_Health health = collision.gameObject.GetComponent<Tank_Health>();
            health.TakeDamage(5);
        }
        if (collision.tag == "Enemy")
        {
            Enemy_Health health1 = collision.gameObject.GetComponent<Enemy_Health>();
            health1.TakeDamage(5);
        }
        
    }
}
