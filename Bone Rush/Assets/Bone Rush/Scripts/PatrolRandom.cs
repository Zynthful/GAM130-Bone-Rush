using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRandom : MonoBehaviour
{
    public NavMeshAgent agent;
    public int wait_time = 3;
    Vector3[] destinations = new[] {new Vector3(20f, 0, 20f),
        new Vector3(20f, 1.5f, -20f),
        new Vector3(-20f, 1.5f, -20f),
        new Vector3(-20f, 1.5f, 20f)};

    private int random_spot;
    private int last_spot;

    //stops AI at patrol points
    IEnumerator WaitTime()
    {
        yield return new WaitForSecondsRealtime(wait_time);
        agent.SetDestination(destinations[random_spot]);
        last_spot = random_spot;
    }

    // Start is called before the first frame update
    //gives the AI a new random destination
    void Start()
    {
        random_spot = Random.Range(0, destinations.Length);
        agent.SetDestination(destinations[random_spot]);
        last_spot = random_spot;
    }

    // Update is called once per frame
    //gives the AI a new random destination
    void Update()
    {
        Vector3 current_location = transform.position;
        if (current_location.x == destinations[random_spot].x && current_location.z == destinations[random_spot].z)
        {
            //makes sure the same spot is not checked twice in a row
            while (random_spot == last_spot)
            {
                random_spot = Random.Range(0, destinations.Length);
            }
            StartCoroutine(WaitTime());
        }

    }
}
