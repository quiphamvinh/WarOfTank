using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    public delegate void BossDie(Boss_Health boss);
    public static event BossDie OnBossDie;
    public float maxHealth;
    public float currentHealth;
    public GameObject explosionPrefab;
    public GameObject dropItem;
    public Animator anim;
    bool isDead;
    AudioManager audioManager;

    public Slider healthBar;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        healthBar = GameObject.FindWithTag("HealthBar_Boss").GetComponent<Slider>();

    }
    private void Start()
    {
        healthBar.value = maxHealth;
        currentHealth = maxHealth;
        isDead = false;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.value = currentHealth / maxHealth;
        if (currentHealth <= 0 && !isDead)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
            Die();
        }
        else
        {
            audioManager.PlaySFX(audioManager.hurt);
            anim.SetTrigger("hurt");
        }
    }

    void Die()
    {
        isDead = true;
        audioManager.PlaySFX(audioManager.death);
        if (OnBossDie != null)
        {
            OnBossDie(this);
        }
        Destroy(gameObject);
    }
}