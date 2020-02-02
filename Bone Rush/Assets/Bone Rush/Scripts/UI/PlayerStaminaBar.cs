using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerStaminaBar : MonoBehaviour
{

    [Header("References")]
    public Slider staminaBar;
    public RigidbodyFirstPersonController movementScript;

    [Header("Values")]
    public float maxStamina = 100f;
    public float staminaDecrease = 10f;
    public float staminaRegen = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();
    }

    void UpdateSlider()
    {
        if (movementScript.Running == true)
        {
            staminaBar.value -= Time.deltaTime * staminaDecrease;
        }
        else
        {
            if (staminaBar.value < maxStamina)
            {
                staminaBar.value += Time.deltaTime * staminaRegen;
            }
        }
    }
}
