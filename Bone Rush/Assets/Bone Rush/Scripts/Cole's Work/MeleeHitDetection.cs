using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitDetection : MonoBehaviour
{

    PlayerHealth ph;
    SwordThings swordAttackScript;
    [SerializeField]
    int damageToDo = 10;
    float playerGracePeriod; // Stops consecutive hits

    // Start is called before the first frame update
    void Start()
    {
        ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        if (CompareTag("Player"))
        {
            swordAttackScript = GetComponentInParent<SwordThings>();
        }
        else
        {
            swordAttackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordThings>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerGracePeriod >= 0)
        {
            playerGracePeriod -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root != transform.root && (CompareTag("Player") || CompareTag("Boss") || CompareTag("Enemy"))) // Checks that it is not colliding with the player
        {
            if (CompareTag("Player"))            
            {
                if (swordAttackScript.swordAnimation.GetBool("Swing"))
                {
                    switch (other.gameObject.tag)
                    {
                        case ("Boss"):
                            other.gameObject.GetComponent<UpdateBossHealth>().UpdateHealth(damageToDo);
                            break;
                        case ("Enemy"):
                            Destroy(other.gameObject);
                            break;
                    }
                }
            }
            else if(CompareTag("Boss"))
            {
                if (GetComponentInParent<Boss>().attacking && playerGracePeriod <= 0 && other.gameObject.CompareTag("Player")){
                    if (!swordAttackScript.isBlocking)
                    {
                        ph.playerHealth -= damageToDo;
                    }
                    else
                    {
                        swordAttackScript.Block(damageToDo, true);
                    }
                    playerGracePeriod = 0.5f;
                }
            }
            else
            {
                if (GetComponentInParent<StateMachine>().attacking && playerGracePeriod <= 0 && other.gameObject.CompareTag("Player"))
                {
                    if (!swordAttackScript.isBlocking)
                    {
                        ph.playerHealth -= damageToDo;
                    }
                    else
                    {
                        swordAttackScript.Block(damageToDo);
                    }
                    playerGracePeriod = 0.5f;
                }
            }
        }
    }
}