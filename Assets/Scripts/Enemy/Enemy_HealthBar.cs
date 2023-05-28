using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy_HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;
    public float smooth = 26f;

    void Start()
    {
        SetHealth(GetComponentInParent<Enemy_Health>().currentHealth, GetComponentInParent<Enemy_Health>().maxHealth);
    }

    public void SetHealth(float health, float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
        if (health <= 0f)
        {
            
        }
    }

    void Update()
    {
        if (transform.parent == null || slider.value <= 0f)
        {
            return;
        }
        //slider.transform.position = Vector3.Lerp(slider.transform.position, Camera.main.WorldToScreenPoint(transform.parent.position + offset), Time.deltaTime * smooth);
    }
}