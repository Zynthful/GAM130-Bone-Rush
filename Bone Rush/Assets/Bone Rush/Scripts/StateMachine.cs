//Code adapted from https://unity3d.college/2019/04/28/unity3d-ai-with-state-machine-drones-and-lasers/
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
    private float follow_distance = 10f;
    private int set_path = 0;
    private float look_range = 25f;
    public float rotation_speed = 35;
    private GameObject player;
    //public float time;

    private void Update()
    {
        switch (_currentState)
        {
            case State.Patrol:
                {
                    player = GameObject.FindWithTag("Player");
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
                        _currentState = State.Search;
                    }


                    if (distance_to_player <= follow_distance)
                    {
                        _currentState = State.Follow;
                    }

                    break;
                }

            case State.Search:
                {
                    transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime, Space.World);

                    int layerMask = 1 << 10;
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, look_range, layerMask))
                    {
                        //Debug.Log("Wall found");
                    }
                    else
                    {
                        layerMask = 1 << 8;
                        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, look_range, layerMask))
                        {
                            //Debug.Log("Player found");
                            _currentState = State.Follow;
                        }
                    }

                    float current_rotation = transform.eulerAngles.y;
                    float stopping_rotation = 0;
                    if (Mathf.Round(current_rotation) == stopping_rotation)
                    {
                        _currentState = State.Patrol;
                    }

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

                    if (distance_to_player > (follow_distance * 2))
                    {
                        _currentState = State.Patrol;
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
        Search,
        Follow,
        Attack
    }
}