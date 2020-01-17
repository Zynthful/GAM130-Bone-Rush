using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public int wait_time = 10;
    Vector3[] destinations = new[] {new Vector3(20f, 0, 20f),
        new Vector3(20f, 1.5f, -20f),
        new Vector3(-20f, 1.5f, -20f),
        new Vector3(-20f, 1.5f, 20f)};

    public int set_path = 0;


    //this function is used to stop the AI after it reaches a patrol point 
    IEnumerator WaitTime()
    {
        yield return new WaitForSecondsRealtime(wait_time);
        agent.SetDestination(destinations[set_path]);
    }

    // Start is called before the first frame update
    //sets a destination for the AI
    void Start()
    {
        agent.SetDestination(destinations[set_path]);
    }

    // Update is called once per frame
    //changes the destination for the AI, works in a loop 0,1,2,3,0...
    void Update()
    {
        Vector3 current_location = transform.position;
        if (current_location.x == destinations[set_path].x && current_location.z == destinations[set_path].z)
        {


            if (set_path == 3)
            {
                set_path = -1;
            }
            set_path += 1;
            StartCoroutine(WaitTime()); 
        }
    }
}

