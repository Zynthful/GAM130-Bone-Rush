using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeleton;
    public int amount_of_enemies = 1;
    public float spawn_rate = 5f;
    private float timer;
    private GameObject spawner;

    //spawns the first enemy once the game starts
    //if only one enemy is spawned then the game object is deactivated
    private void Start()
    {
        if (amount_of_enemies >= 1)
        {
            SpawnEnemy();
        }
        deactivate();
    }

    //spawn if more then one enemies has been selected
    //uses a timer to make enemies not all group up
    //deactivates the game object once all enemies are spawned
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawn_rate && amount_of_enemies >= 1)
        {
            timer -= spawn_rate;
            SpawnEnemy();
            deactivate();
        }
    }

    //spawns the enemies into the scene
    private void SpawnEnemy()
    {
        amount_of_enemies -= 1;
        Instantiate(skeleton, transform.position, Quaternion.identity);
    }

    private void deactivate()
    {
        if (amount_of_enemies == 0)
        {
            spawner = GameObject.FindWithTag("Spawner");
            spawner.SetActive(false);
        }
    }
}