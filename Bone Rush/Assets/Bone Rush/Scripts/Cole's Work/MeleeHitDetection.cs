using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitDetection : MonoBehaviour
{

    SwordThings swordAttackScript;

    // Start is called before the first frame update
    void Start()
    {
        swordAttackScript = GetComponentInParent<SwordThings>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root != transform.root && swordAttackScript.swordAnimation.GetBool("Swing")) // Checks that it is not colliding with the player
        {
            if (other.gameObject.CompareTag("Enemy"))            
            {
                //Destroy(other.gameObject);
            }
        }
    }
}