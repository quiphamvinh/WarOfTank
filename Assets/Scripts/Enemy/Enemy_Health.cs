using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour
{
    public delegate void EnemyDieEvent(Enemy_Health enemy);
    public static event EnemyDieEvent OnEnemyDie;
    public float maxHealth;
    public float currentHealth;
    public GameObject explosionPrefab;
    public GameObject dropItem;
    public Animator anim;
    bool isDead;
    AudioManager audioManager;
    public Enemy_HealthBar HealthBar;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        HealthBar.SetHealth(currentHealth, maxHealth);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
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
        HealthBar.SetHealth(currentHealth, maxHealth);
    }

    void Die()
    {
        isDead = true;
        
        if (OnEnemyDie != null)
        {
            OnEnemyDie(this);
            audioManager.PlaySFX(audioManager.death);
        }
        Destroy(gameObject);
    }
}