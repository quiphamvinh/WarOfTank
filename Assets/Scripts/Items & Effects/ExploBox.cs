using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExploBox : MonoBehaviour
{
    [SerializeField] private GameObject fireFX;
    AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tank_Bullet"))
        {
            audioManager.PlaySFX(audioManager.firegas);
            Destroy(gameObject);
            GameObject fire = Instantiate(fireFX, transform.position, Quaternion.identity);
            Destroy(fire, 5f);
        }

        if (collision.gameObject.CompareTag("Enemy_Bullet"))
        {
            audioManager.PlaySFX(audioManager.firegas);
            Destroy(gameObject);
            GameObject fire = Instantiate(fireFX, transform.position, Quaternion.identity);
            Destroy(fire, 5f);
        }
    }
}
