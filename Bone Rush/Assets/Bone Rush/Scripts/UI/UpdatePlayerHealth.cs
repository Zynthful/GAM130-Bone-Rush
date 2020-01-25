using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class UpdatePlayerHealth : MonoBehaviour
{
    // public HealthScriptPlaceholder HealthScriptPlaceholder;

    Slider PlayerHealth;
    float TestTimer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // TestTimer function does damage to the player on a 2s timer for debugging
        // if(TestTimer <= 0)
        // {
        //    int Damage = Random.Range(1, 10);
        //    UpdateHealth(Damage);
        //    TestTimer = 2f;
        //    Debug.Log("Player took: " + Damage + " Damage to HP");
        //    Debug.Log("Player Current HP: " + PlayerHealth.value);
        // }
        // else
        // {
        //    TestTimer -= Time.deltaTime;
        // }

        if(PlayerHealth.value <= 0)
        {

        }
    }
    public void UpdateHealth(int Damage)
    {
        PlayerHealth.value -= Damage;
    }
}
