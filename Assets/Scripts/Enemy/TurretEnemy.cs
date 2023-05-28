using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    private GameObject player;
    public Animator anim;
    public GameObject item;
    AudioManager audioManager;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            
            if (distance < 17)
            {
                Vector3 dir = player.transform.position - transform.position;
                float rot = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rot+270);
                timer += Time.deltaTime;
                if (timer > 1.5f)
                {
                    timer = 0;
                    Shoot();
                }
            }
        }        
    }

    void Shoot()
    {       
        anim.SetTrigger("IsFire");
        audioManager.PlaySFX(audioManager.Shoot);
        Instantiate(bullet, bulletPos.position, Quaternion.identity);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Tank_Bullet"))
        {
            Destroy(this.gameObject);
            Instantiate(item, transform.position, Quaternion.identity);
            audioManager.PlaySFX(audioManager.death);
        }
    }

}
