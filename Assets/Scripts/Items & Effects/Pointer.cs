using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject player;
    public GameObject chest;
    public GameObject arrowPrefab;
    public float arrowSpeed = 5f;
    private GameObject arrow;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chest = GameObject.FindGameObjectWithTag("Finish");
        if (chest != null && player != null)
        {
            float distance = Vector3.Distance(player.transform.position, chest.transform.position);
            if (distance <= 15f)
            {
                if (arrow != null)
                {
                    Destroy(arrow);
                }
            }

            else if (distance < 100f)
            {
                if (arrow == null)
                {
                    arrow = Instantiate(arrowPrefab);
                }

                Vector3 direction = (chest.transform.position - player.transform.position).normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                arrow.transform.position = player.transform.position + direction * 7f;
                arrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;
            }
            else
            {
                if (arrow != null)
                {
                    Destroy(arrow);
                }
            }
        }
    }

}
