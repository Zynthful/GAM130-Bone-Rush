//Code adapted from https://unity3d.college/2019/04/28/unity3d-ai-with-state-machine-drones-and-lasers/  
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
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
    private float current_rotation;
    private bool location_set = false;
    private float stopping_rotation;
    private float enemy_current_rotation;
    private float player_health = 0;
    public Animator swing;
    private const float attackDelayReset = 2f;
    private float attackDelay;
    public bool damage;

    [Header("Attacking Variables")]
    public PlayerHealth ph;
    private SwordThings st;

    //checks to see if the enemy has been attacked by a player weapon
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerWeapon" && st.swordAnimation.GetBool("Swing"))
        {
            Debug.Log("hit detected");
            damage = true;
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        ph = player.GetComponent<PlayerHealth>();
        st = player.GetComponent<SwordThings>();
    }

    private void Update()
    {
        swing = GameObject.Find("Handle").GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        Vector3 enemy_location = destinations[set_path].transform.position;
        Vector3 current_location = transform.position;
        if(attackDelay > 0)
        {
            swing.SetBool("Attacking", false);
            attackDelay -= Time.deltaTime;
        }

        switch (_currentState)
        {
            //follows the player unless they go out of range of the enemy
            //if the player gets far enough away the enemy goes back to patrolling
            case State.Follow:
                {
                    //Debug.Log("follow");
                    float distance_to_player = Vector3.Distance(transform.position, player.transform.position);
                    Vector3 player_location = player.transform.position;
                    agent.SetDestination(player_location);

                    if (distance_to_player <= 3)
                    {
                        _currentState = State.Attack;
                    }

                    break;

                }
            //placeholder for now, enemy attacks the player until one dies
            case State.Attack:
                {
                    //Debug.Log("attack");
                    if (attackDelay <= 0)
                    {
                        //attack always hits, need to check if attack hits
                        //boss weapons collier is disabled to help boss more better
                        agent.SetDestination(current_location);
                        swing.SetBool("Attacking", true);     
                        _currentState = State.Retreat;
                        attackDelay = attackDelayReset;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            case State.Retreat:
                {
                    //Debug.Log("retreat");
                    agent.SetDestination(enemy_location);
                    if (current_location.x == enemy_location.x && current_location.z == enemy_location.z)
                    {
                        _currentState = State.Follow;
                    }
                    break;
                }
        }
    }

    public enum State
    {
        Follow,
        Attack,
        Retreat
    }
}