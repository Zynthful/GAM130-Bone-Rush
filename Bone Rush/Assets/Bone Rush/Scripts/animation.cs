using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    public Animator swing;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Handle").GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("BossSwing");
    }
}
