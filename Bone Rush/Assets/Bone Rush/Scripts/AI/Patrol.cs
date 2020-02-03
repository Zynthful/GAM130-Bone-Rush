using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    private GameObject[] destinations;
    [SerializeField]
    private GameObject player;
    private int set_path = 0;
    private float follow_distance = 10f;
    public NavMeshAgent agent;

    public void patrol()
    {

        Vector3 enemy_location = destinations[set_path].transform.position;
        float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
        Vector3 current_location = transform.position;

        agent.SetDestination(enemy_location);
        if (current_location.x == enemy_location.x && current_location.z == enemy_location.z)
        {
            if (set_path == (destinations.Length - 1))
            {
                set_path = -1;
            }
            set_path += 1;
            Debug.Log("search");
            //_currentState = State.Search;
        }


        if (distance_to_player <= follow_distance)
        {
            Debug.Log("follow");
            //_currentState = State.Follow;
        }
    }
}
