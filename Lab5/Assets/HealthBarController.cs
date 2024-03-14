using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Slider = UnityEngine.UI.Slider;

public class HealthBarController : MonoBehaviour
{
    Slider healthBar;
  
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.value = currentHealth = 100;
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
    }
}
