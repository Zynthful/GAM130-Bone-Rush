using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public NavMeshAgent agent;

    public int wait_time = 3;
    private bool moving = false; 
    Vector3[] destinations = new[] {new Vector3(16.13575f, 28.16976f, 60.23705f),
        new Vector3(20.13575f, 28.16976f, 60.23705f) };
    private int random_spot;
    public bool loop = true;


    public Vector3 new_location;


    // Start is called before the first frame update
    void Start()
    {
        random_spot = Random.Range(0, destinations.Length);
        Debug.Log(destinations[random_spot]);
        //Debug.Log(destinations[random_spot].GetType().FullName);
        //agent.SetDestination(new Vector3(16.13575f, 28.16976f, 60.23705f));


        //Vector3 temp = transform.position;
        //temp.x -= 20.0f;
        //new_location = temp;

        //agent.SetDestination(new_location);

    }

    // Update is called once per frame
    void Update()
    {

        //yield return new WaitUntil(() => transform.position == new_location);

        /*
        if (transform.position == destinations[random_spot])
        {
            agent.SetDestination(destinations[random_spot]);
        }
        else
        {
            moving = true;
        }
        */

    }
}
