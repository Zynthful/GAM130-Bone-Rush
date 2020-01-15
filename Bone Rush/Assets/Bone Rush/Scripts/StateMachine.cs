using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{

    Vector3[] destinations = new[] {new Vector3(20f, 0, 20f),
        new Vector3(20f, 1.5f, -20f),
        new Vector3(-20f, 1.5f, -20f),
        new Vector3(-20f, 1.5f, 20f)};
    private State _currentState;
    public NavMeshAgent agent;
    public GameObject player;
    public float follow_distance = 10f;
    public int set_path = 0;
    public int wait_time = 3;

    IEnumerator WaitTime()
    {
        yield return new WaitForSecondsRealtime(wait_time);
        agent.SetDestination(destinations[set_path]);
    }


    private void Update()
    {
        switch (_currentState)
        {
            case State.Patrol:
                {
                    agent.SetDestination(destinations[set_path]);
                    float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
                    Vector3 current_location = transform.position;
                    if (current_location.x == destinations[set_path].x && current_location.z == destinations[set_path].z)
                    {
                        if (set_path == 3)
                        {
                            set_path = -1;
                        }
                        set_path += 1;
                        StartCoroutine(WaitTime());
                    }


                    if (distance_to_player <= follow_distance)
                    {
                        _currentState = State.Follow;
                    }

                    break;
                }

            case State.Patrol_Random:
                {
                    break;
                }

            case State.Follow:
                {
                    float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
                    Vector3 player_location = player.transform.position;
                    agent.SetDestination(player_location);

                        if (distance_to_player <= 1)
                        {
                            _currentState = State.Attack;
                        }
                    break;

                }
            case State.Attack:
                {
                    //Debug.Log("attacking");
                    player.active = false;
                    break;
                }
        }
    }


    public enum State
    {
        Patrol,
        Patrol_Random,
        Follow,
        Attack
    }
}