using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject[] items;
    AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {     
        if (collision.gameObject.CompareTag("Tank_Bullet"))
        {
            Destroy(gameObject);
            audioManager.PlaySFX(audioManager.box);
            if (items != null && items.Length > 0)
            {
                GameObject selectedItem = items[Random.Range(0, items.Length)];
                Instantiate(selectedItem, transform.position, Quaternion.identity);
            }
        }
    }
}