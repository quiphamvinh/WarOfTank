using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank_Health : MonoBehaviour
{
    public float maxHealth=15f;
    public float currentHealth;
    public GameObject explosionPrefab;
    public Animator anim;

    private Slider healthSlider;
    private bool isDead = false;
    private GameManager GameManager;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        GameObject healthBarObject = GameObject.FindWithTag("HealthBar");
        if (healthBarObject != null)
        {
            healthSlider = healthBarObject.GetComponent<Slider>();
            if (healthSlider != null)
            {
                healthSlider.maxValue = maxHealth;
                healthSlider.value = currentHealth;
            }
        }

        GameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            if (healthSlider != null)
            {
                healthSlider.value = currentHealth;
            }

            if (currentHealth <= 0)
            {
                isDead = true;
                currentHealth = 0;
                audioManager.PlaySFX(audioManager.death);
                healthSlider.value = currentHealth;

                GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
                Destroy(explosion, 2f);
                Destroy(gameObject);


                if (GameManager != null && GameManager.IsGameWin())
                {
                    GameManager.instance.ShowGameWin();
                    Time.timeScale = 0f;
                }
                else
                {
                    GameManager.instance.ShowGameOver();
                    Time.timeScale = 0f;
                }
            }
            else 
            {
                audioManager.PlaySFX(audioManager.hurt);
                anim.SetTrigger("hurt");
            }
        }
    }

    public void AddHeal(float value)
    {
        if (!isDead)
        {
            currentHealth += value;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            audioManager.PlaySFX(audioManager.addHealth);
            if (healthSlider != null)
            {
                healthSlider.value = currentHealth;
            }
        }
    }
}
