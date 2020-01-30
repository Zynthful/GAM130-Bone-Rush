//Code adapted from https://unity3d.college/2019/04/28/unity3d-ai-with-state-machine-drones-and-lasers/  
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public GameObject[] destinations;
    private State _currentState;
    public NavMeshAgent agent;
    private float follow_distance = 10f;
    private int set_path = 0;
    private float look_range = 25f;
    public float rotation_speed = 35;
    private GameObject player;
    float current_rotation;
    bool location_set = false;
    float stopping_rotation;
    float enemy_current_rotation;
    private float player_health = 0;
    public Animator swing;

    //checks to see if the enemy has been attacked by a player weapon
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player Weapon")
        {
            Debug.Log("ow");        //enemy take damage
        }
    }

    private void Update()
    {
        swing = GameObject.Find("Handle").GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        Vector3 enemy_location = destinations[set_path].transform.position;
        Vector3 current_location = transform.position;
        switch (_currentState)
        {
            //in this state the enemy walks from one marker to another, then searches for the player
            //will follow the player if they get too close
            case State.Patrol:
                {
                    agent.SetDestination(enemy_location);
                    float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
                    if (current_location.x == enemy_location.x && current_location.z == enemy_location.z)
                    {
                        if (set_path == (destinations.Length-1))
                        {
                            set_path = -1;
                        }
                        set_path += 1;
                    }


                    if (distance_to_player <= follow_distance)
                    {
                        _currentState = State.Follow;
                    }

                    break;
                }

            //the enemy rotates 360 degrees and follows the player if they are within the enemies line of sight
            case State.Search:
                {
                    transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime, Space.World);

                    int layerMask = 1 << 10;
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, look_range, layerMask))
                    {
                        //Wall found
                    }
                    else
                    {
                        layerMask = 1 << 8;
                        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, look_range, layerMask))
                        {
                            _currentState = State.Follow;       //found player
                        }
                    }

                    enemy_current_rotation = transform.eulerAngles.y;
                    if (location_set == false)
                    {
                        stopping_rotation = Mathf.Round(enemy_current_rotation) - 1;
                        location_set = true;
                    }
                    if (Mathf.Round(enemy_current_rotation) == stopping_rotation)
                    {
                        location_set = false;
                        _currentState = State.Patrol;
                    }
                    break;
                }

            //follows the player unless they go out of range of the enemy
            //if the player gets far enough away the enemy goes back to patrolling
            case State.Follow:
                {
                    float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
                    Vector3 player_location = player.transform.position;
                    agent.SetDestination(player_location);

                    if (distance_to_player <= 4)
                    {
                        _currentState = State.Attack;
                    }

                    if (distance_to_player > (follow_distance * 2))
                    {
                        _currentState = State.Patrol;
                    }
                    break;

                }
            //placeholder for now, enemy attacks the player until one dies
            case State.Attack:
                {
                    agent.SetDestination(current_location);
                    swing.Play("BossSwing");
                    //player.SetActive(false);      
                    if (player_health < 1)
                    {
                        //SceneManager.LoadScene("SCN_Menu_Defeat");
                    }
                    _currentState = State.Retreat;
                    break;
                }
            case State.Retreat:
                {
                    agent.SetDestination(enemy_location);
                    if (current_location.x == enemy_location.x && current_location.z == enemy_location.z)
                    {
                        _currentState = State.Patrol;
                    }
                    break;
                }
        }
    }

    public enum State
    {
        Patrol,
        Search,
        Follow,
        Attack,
        Retreat
    }
}