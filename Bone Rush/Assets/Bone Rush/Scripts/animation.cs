using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    private Animator anim;

    private float testDelay;
    private float switchIdle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Handle").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (testDelay <= 0)
        {
            anim.SetBool("Attacking", true);
            testDelay = 2f;
            switchIdle = .5f;
        }
        else
        {
            testDelay -= Time.deltaTime;
            switchIdle -= Time.deltaTime;
        }

        if(switchIdle <= 0)
        {
            anim.SetBool("Attacking", false);
        }
    }
}
