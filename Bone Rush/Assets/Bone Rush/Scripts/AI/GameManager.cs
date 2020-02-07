using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Patrol patrol;


    // Start is called before the first frame update
    void Start()
    {
        patrol = GetComponent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        //patrol;
    }
}
