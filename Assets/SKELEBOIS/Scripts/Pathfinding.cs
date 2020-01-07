using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    /*
    void Start()
    {
        Vector3 enemy_location = transform.position;
        Vector3 player_location = player.transform.position;
    }
    */

    // Update is called once per frame
    void Update()
    {
        Vector3 player_location = player.transform.position;
        agent.SetDestination(player_location);
    }
}
