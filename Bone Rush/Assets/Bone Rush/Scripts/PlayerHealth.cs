using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100;
    public int damageTaken = 25;
    public Slider healthSlider;

    public Animator swing;
    public Boss Boss;

    //checks to see if the player has been attacked the boss
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss" && Boss.swing.GetBool("Attacking") == true)//and boss is doing swing animation
        {
            Debug.Log("player damaged by boss"); //still triggers if player sword hit the boss
            playerHealth -= damageTaken;
        }
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("SCN_Menu_Defeat");
        }
    }

    private void Update()
    {
        swing = GameObject.Find("Handle").GetComponent<Animator>();
        healthSlider.value = playerHealth;
    }
}
    