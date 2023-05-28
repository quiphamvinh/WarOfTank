using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private GameObject num;
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.numberOfCoins += value;
            PlayerPrefs.SetInt("NumberOfCoins", GameManager.numberOfCoins);
            audioManager.PlaySFX(audioManager.addCoin);            
            Destroy(gameObject);
            Destroy(Instantiate(num, collision.transform.position, Quaternion.identity),1f);
        }
    }
}
