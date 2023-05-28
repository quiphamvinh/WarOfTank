using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class Enemy_AI : MonoBehaviour
{
    public AIPath aIPath;
    public AIDestinationSetter destination;
    Transform player;
    Vector2 direction;

    public List<Transform> firePoints = new List<Transform>();
    public List<Animator> Effectfire = new List<Animator>();
    public GameObject bulletPrefab;

    public float bulletSpeed = 12f;
    public float fireRate = 1.5f;
    public float shootDistance = 10f;
    float nextFire;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        destination.target = player;
    }

    void Update()
    {
        direction = aIPath.desiredVelocity;
        this.transform.right = direction;
        chaseTarget();
    }

    void chaseTarget()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= shootDistance && Time.time >= nextFire)
            {
                foreach (Transform point in firePoints)
                { 
                    RaycastHit2D hit = Physics2D.Raycast(point.position, player.position - transform.position, shootDistance);
                    if (hit.collider != null && hit.collider.CompareTag("Player"))
                    {
                        fireShoot();
                        nextFire = Time.time + 1.5f / fireRate;
                    }
                }
            }
        }
    }

    void fireShoot()
    {
        foreach (Animator animator in Effectfire)
        {
            animator.SetTrigger("isFire");
            audioManager.PlaySFX(audioManager.Shoot);
        }
        foreach (Transform point in firePoints)
        {
            GameObject newBullet = Instantiate(bulletPrefab, point.position, point.rotation);
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(point.right * 20f);
        }
    }
}