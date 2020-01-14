using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    //will only chase the player if they enter their view radius
    //player can be seen through walls and is followed until the AI dies
    public NavMeshAgent agent;
    public GameObject player;
    public float follow_distance = 10f;

    // Update is called once per frame
    void Update()
    {  
        float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
        if (distance_to_player <= follow_distance)
        {    
            Vector3 player_location = player.transform.position;
            agent.SetDestination(player_location);

            if (distance_to_player <= 1)        //attacks the player
            {
                //Debug.Log("Can attack");
                player.active = false;
            }
        }
    }  
}
