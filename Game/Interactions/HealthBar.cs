using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public PlayerHealth healthScript;

    private void Start()
    {
        healthBar.maxValue = healthScript.MaxHealth;
        healthBar.value = healthScript.MaxHealth;
    }

    private void Update()
    {
        healthBar.value = healthScript.currentHealth;
    }
}
