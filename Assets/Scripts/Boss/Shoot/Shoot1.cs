using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1 : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;
    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;
    [SerializeField]
    private float firingDelay = 0.2f;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, firingDelay);
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;
        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;
            GameObject bul = Boss_BulletPool.instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Boss_Bullet>().SetMoveDirection(bulDir);
            angle += angleStep;
        }
    }
    public void StopShooting()
    {
        CancelInvoke("Fire");
    }

    public void StartShooting()
    {
        InvokeRepeating("Fire", 0f, firingDelay);
    }
    public void SetFiringDelay(float delay)
    {
        firingDelay = delay;
        CancelInvoke("Fire");
        InvokeRepeating("Fire", 0f, firingDelay);
    }

}