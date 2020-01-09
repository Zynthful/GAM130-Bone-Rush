using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineOfSight : MonoBehaviour
{
    //will only chase the player if they enter their line of sight 
    //struggles to chase players when they turn
    public NavMeshAgent agent;
    public GameObject player;
    public float rotation_speed = 35;
    public bool found_player;

    // Update is called once per frame
    void Update()
    {
        if (found_player == true)
        {
            Vector3 player_location = player.transform.position;
            agent.SetDestination(player_location);
        }

        else
        {
            transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime, Space.World);

            int layerMask = 1 << 8;     //makes only layer 8 (player layer) will be effected by the ray
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Player Found");
                found_player = true;
            }
            /*
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
            */
        }
    }
}
