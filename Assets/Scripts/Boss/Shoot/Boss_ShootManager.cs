using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ShootManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] styleObjects;

    private int currentStyleIndex = 0;
    private Coroutine changeStyleCoroutine;

    private void Start()
    {
        changeStyleCoroutine = StartCoroutine(ChangeStyleAndStopShooting());
    }

    private IEnumerator ChangeStyleAndStopShooting()
    {
        GameObject currentStyleObj = styleObjects[currentStyleIndex];
        GameObject previousStyleObj = null;

        yield return new WaitForSeconds(8f);

        while (true)
        {
            if (previousStyleObj != null)
            {
                Shoot1 prevShoot1 = previousStyleObj.GetComponent<Shoot1>();
                if (prevShoot1 != null) prevShoot1.StopShooting();
                Shoot2 prevShoot2 = previousStyleObj.GetComponent<Shoot2>();
                if (prevShoot2 != null) prevShoot2.StopShooting();
                Shoot3 prevShoot3 = previousStyleObj.GetComponent<Shoot3>();
                if (prevShoot3 != null) prevShoot3.StopShooting();
            }

            Shoot1 currShoot1 = currentStyleObj.GetComponent<Shoot1>();
            if (currShoot1 != null) currShoot1.StartShooting();
            Shoot2 currShoot2 = currentStyleObj.GetComponent<Shoot2>();
            if (currShoot2 != null) currShoot2.StartShooting();
            Shoot3 currShoot3 = currentStyleObj.GetComponent<Shoot3>();
            if (currShoot3 != null) currShoot3.StartShooting();
            yield return new WaitForSeconds(8f);

            currShoot1 = currentStyleObj.GetComponent<Shoot1>();
            if (currShoot1 != null) currShoot1.StopShooting();
            currShoot2 = currentStyleObj.GetComponent<Shoot2>();
            if (currShoot2 != null) currShoot2.StopShooting();
            currShoot3 = currentStyleObj.GetComponent<Shoot3>();
            if (currShoot3 != null) currShoot3.StopShooting();
            yield return new WaitForSeconds(5f);

            previousStyleObj = currentStyleObj;
            currentStyleIndex = (currentStyleIndex + 1) % styleObjects.Length;
            currentStyleObj = styleObjects[currentStyleIndex];

            yield return new WaitForSeconds(3f);
        }
    }

    public void Reset()
    {
        StopCoroutine(changeStyleCoroutine);
        foreach (GameObject styleObj in styleObjects)
        {
            Shoot1 shoot1 = styleObj.GetComponent<Shoot1>();
            if (shoot1 != null) shoot1.StopShooting();
            Shoot2 shoot2 = styleObj.GetComponent<Shoot2>();
            if (shoot2 != null) shoot2.StopShooting();
            Shoot3 shoot3 = styleObj.GetComponent<Shoot3>();
            if (shoot3 != null) shoot3.StopShooting();
            styleObj.SetActive(false);
        }
        currentStyleIndex = 0;
        styleObjects[currentStyleIndex].SetActive(true);
        StartCoroutine(ChangeStyleAndStopShooting());
    }
}