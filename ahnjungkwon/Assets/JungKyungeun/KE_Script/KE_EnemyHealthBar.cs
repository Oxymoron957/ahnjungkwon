using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KE_EnemyHealthBar : MonoBehaviour
{
    public int health;
    public int maxHealth=3;

    public GameObject healthBarUI;
    public Slider slider;

    private void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();

    }

    private void Update()
    {
        slider.value = CalculateHealth();

       
        
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }
}
