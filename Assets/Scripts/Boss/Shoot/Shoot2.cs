using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot2 : MonoBehaviour
{

    [SerializeField]
    private float angle = 0f;
    [SerializeField]
    private float firingDelay = 0.2f;
    [SerializeField]
    private Transform[] firePoints;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, firingDelay);
    }

    private void Fire()
    {
        foreach (Transform firePoint in firePoints)
        {
            float bulDirX = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = firePoint.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - firePoint.position).normalized;
            GameObject bul = Boss_BulletPool.instance.GetBullet();
            bul.transform.position = firePoint.position;
            bul.transform.rotation = firePoint.rotation;
            bul.SetActive(true);
            bul.GetComponent<Boss_Bullet>().SetMoveDirection(bulDir);
        }
        angle += 10f;
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

    public void SetFirePoints(Transform[] points)
    {
        firePoints = points;
    }

}