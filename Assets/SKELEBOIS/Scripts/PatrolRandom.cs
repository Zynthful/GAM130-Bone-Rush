using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRandom : MonoBehaviour
{
    public NavMeshAgent agent;
    Vector3[] destinations = new[] {new Vector3(20f, 0, 20f),
        new Vector3(20f, 1.5f, -20f),
        new Vector3(-20f, 1.5f, -20f),
        new Vector3(-20f, 1.5f, 20f),};

    private int random_spot;


    // Start is called before the first frame update
    void Start()
    {
        random_spot = Random.Range(0, destinations.Length);
        agent.SetDestination(destinations[random_spot]);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current_location = transform.position;
        if (current_location.x == destinations[random_spot].x && current_location.z == destinations[random_spot].z)
        {
            random_spot = Random.Range(0, destinations.Length);
            agent.SetDestination(destinations[random_spot]);
        }

    }
}
