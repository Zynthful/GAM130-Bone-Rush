using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    
public class UpdateBossHealth : MonoBehaviour
{
    // public HealthScriptPlaceholder HealthScriptPlaceholder;

    GameObject Boss;
    Slider BossHealth;
    float TestTimer;

    // Start is called before the first frame update
    void Start()
    {
        BossHealth = GetComponent<Slider>();
        Boss = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        // TestTimer function does damage to the boss on a 2s timer for debugging
        if(TestTimer <= 0)
        {
            int Damage = Random.Range(1, 10);
            UpdateHealth(Damage);
            TestTimer = 2f;
            //Debug.Log("Boss took: " + Damage + " Damage to HP");
            //Debug.Log("Boss Current HP: " + BossHealth.value);
        }
        else
        {
            TestTimer -= Time.deltaTime;
        }

        if(BossHealth.value <= 0)
        {
            // Destroy(Boss);
            SceneManager.LoadScene("SCN_Menu_Win");
        }
    }
    public void UpdateHealth(int Damage)
    {
        //BossHealth.value -= Damage;
    }
}
