using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class PlayerStaminaBar : MonoBehaviour
{

    [Header("References")]
    public Slider staminaBar;
    public RigidbodyFirstPersonController movementScript;

    [Header("Values")]
    public float maxStamina = 100f;
    public float staminaDecrease = 10f;
    public float staminaRegen = 2f;
    public float minStamina = 0.5f;
    public float timeBeforeRegen = 1f;
    public bool canRegen;
    public bool canSprint = true;

    void Update()
    {
        CheckForStamina();
        UpdateSlider();
        StaminaRegen();
    }

    void CheckForStamina()
    {
        if (staminaBar.value <= minStamina && movementScript.Running)
        {
            canSprint = false;
        }
        else
        {
            canSprint = true;
        }
    }

    void UpdateSlider()
    {
        if (canSprint)
        {
            if (movementScript.Running)
            {
                staminaBar.value -= Time.deltaTime * staminaDecrease;
            }
        }
        else
        {
            StaminaRegen();
        }
    }

    void StaminaRegen()
    {
        if (staminaBar.value < maxStamina)
        {
            if (movementScript.Running == false)
            {
                StartCoroutine(WaitForRegen());
            }

            if (canRegen)
            {
                staminaBar.value += Time.deltaTime * staminaRegen;
            }
        }
    }

    IEnumerator WaitForRegen()
    {
        yield return new WaitForSeconds(timeBeforeRegen);
        canRegen = true;
    }

}
