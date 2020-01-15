using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineOfSight : MonoBehaviour
{
    //will only chase the player if they enter their line of sight, added extra if statement to stop the AI looking through walls
    //the enemies line of sight can be changed by the look_range variable
    public NavMeshAgent agent;
    public GameObject player;
    public float rotation_speed = 35;
    bool found_player;
    public float look_range = 25f;

    // Update is called once per frame
    void Update()
    {
        if (found_player == true)
        {
            float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
            Vector3 player_location = player.transform.position;
            agent.SetDestination(player_location);
            if (distance_to_player <= 1)        //attacks the player
            {
                //Debug.Log("Can attack");
                player.active = false;
            }
        }

        else
        {
            transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime, Space.World);

            //int layerMask = 8 << 10;     //makes only layers 8-10 be effected by the ray (player, enemies, walls) NOT WORKING ANYMORE???
            int layerMask = 1 << 10;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, look_range, layerMask))
            {
                Debug.Log("Wall");
            }
            layerMask = 1 << 8;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, look_range, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Player Found");
                found_player = true;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
        }
    }
}
