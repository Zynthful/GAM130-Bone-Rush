using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    void Start()
    {
        Vector3 enemy_location = transform.position;
        //Vector3 player_location = ???
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.x);
        //agent.SetDestination(ENTER PLAYERS LOCATION HERE);
        UnityEngine.Debug.Log(player.Transform.position);
    }
}
