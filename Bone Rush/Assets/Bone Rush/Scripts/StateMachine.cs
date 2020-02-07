//Code adapted from https://unity3d.college/2019/04/28/unity3d-ai-with-state-machine-drones-and-lasers/  
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;


public class StateMachine : MonoBehaviour
{
    [Header("Pathfinding Variables")]
    [SerializeField]
    private GameObject[] destinations;
    private State _currentState;
    public NavMeshAgent agent;
    private float follow_distance = 10f;
    private int set_path = 0;
    private float look_range = 25f;
    private float rotation_speed = 35;
    private GameObject player;
    float current_rotation;
    private bool location_set = false;
    private float stopping_rotation;
    private float enemy_current_rotation;

    [Header("Attacking Variables")]
    private PlayerHealth ph;
    private SwordThings st;
    private float attackRate = 1f;
    private bool hasAttacked;
    public bool attacking;
    

    //checks to see if the enemy has been attacked by a player weapon
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerWeapon" && st.swordAnimation.GetBool("Swing"))
        {
            Destroy(this.gameObject);
        }
    }*/

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        ph = player.GetComponent<PlayerHealth>();
        st = player.GetComponent<SwordThings>();
    }

    private void Update()
    {
        
        switch (_currentState)
        {
            //in this state the enemy walks from one marker to another, then searches for the player
            //will follow the player if they get too close
            case State.Patrol:
                {
                    Debug.Log("Patrol");

                    Vector3 enemy_location = destinations[set_path].transform.position;
                    agent.SetDestination(enemy_location);
                    float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
                    Vector3 current_location = transform.position;
                    if (current_location.x == enemy_location.x && current_location.z == enemy_location.z)
                    {
                        if (set_path == (destinations.Length-1))
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

            //the enemy rotates 360 degrees and follows the player if they are within the enemies line of sight
            case State.Search:
                {
                    Debug.Log("Search");
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
                    Debug.Log("Follow");
                    hasAttacked = false;
                    float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
                    Vector3 player_location = player.transform.position;
                    agent.SetDestination(player_location);

                    if (distance_to_player <= 1.5 && !hasAttacked)
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
                    Debug.Log("Attack");
                    float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
                    if (distance_to_player >= 1.5)
                    {
                        _currentState = State.Follow;
                    }

                    if (hasAttacked == false)
                    {
                        attacking = true;
                        if (player.GetComponent<SwordThings>().isBlocking)
                        {
                            player.GetComponent<SwordThings>().Block(ph.damageTaken);
                        }
                        else
                        {
                            ph.playerHealth -= ph.damageTaken;
                        }
                        hasAttacked = true;
                        StartCoroutine(AttackDelay());
                    }                                    


                    if (ph.playerHealth <= 0)
                    {
                        player.SetActive(false);
                        SceneManager.LoadScene("SCN_Menu_Defeat");
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
        Attack
    }

    IEnumerator AttackDelay()
    {
        Debug.Log("ReachedCoroutine");
        yield return new WaitForSeconds(attackRate);
        attacking = false;
        _currentState = State.Search;
    }
}