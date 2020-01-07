using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public float follow_distance = 10f;

    /*
    void Start()
    {
        Vector3 enemy_location = transform.position;
        Vector3 player_location = player.transform.position;
    }
    */

    // Update is called once per frame
    void Update()
    {   //player is followed until the enemy dies even if the player gets out of the enemies range
        float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
        if (distance_to_player <= follow_distance)
        {    //player can be seen through walls
            Vector3 player_location = player.transform.position;
            agent.SetDestination(player_location);
        }
    }  
}
