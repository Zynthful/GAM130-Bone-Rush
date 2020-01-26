using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public int damageTaken = 25;
    public Slider healthSlider;

    private void Update()
    {
        healthSlider.value = playerHealth;
    }
}
    