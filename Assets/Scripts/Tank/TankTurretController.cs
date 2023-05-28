using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretController : MonoBehaviour
{
    public List<Transform> firePoints = new List<Transform>();
    public List<Animator> Effectfire = new List<Animator>();
    public GameObject bullet;
    public FixedJoystick joystick;
    public Animator fire;
    AudioManager audioManager;

    private Vector2 currentDirection = Vector2.right;
    private bool isJoystickReleased = true;
    public float fireRate = 0.5f;
    private float nextFireTime = 0.0f;
    public float turnSpeed = 10.0f;

    void Awake()
    {
        joystick = GameObject.FindWithTag("JoystickTurrent").GetComponent<FixedJoystick>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        FindFirePoints(transform);
    }

    void Update()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            currentDirection = new Vector2(h, v).normalized;
            isJoystickReleased = false;
            transform.right = Vector2.Lerp(transform.right, currentDirection, Time.deltaTime * turnSpeed);
            fire.SetBool("IsFire", true);

        }
        else if (!isJoystickReleased && Time.time >= nextFireTime)
        {
            fire.SetBool("IsFire", false);
            Shoot();
            nextFireTime = Time.time + fireRate;
            isJoystickReleased = true;
        }
        else
        {
            fire.SetBool("IsFire", false);
        }

        var desiredAngle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;      
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, desiredAngle), turnSpeed);
    }

    void Shoot()
    {
        foreach (Animator animator in Effectfire)
        {
            animator.SetTrigger("isFire");
            audioManager.PlaySFX(audioManager.Shoot);
        }
        foreach (Transform point in firePoints)
        {

            GameObject newBullet = Instantiate(bullet, point.position, point.rotation);
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(point.right * 20f);
        }
    }

    void FindFirePoints(Transform parent)
    {
        if (parent.gameObject.CompareTag("FirePoint"))
        {
            firePoints.Add(parent);
        }
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            FindFirePoints(child);
        }
    }
}