using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeleton;
    public int amount_of_enemies = 1;
    public float spawn_rate = 5f;
    private float timer;
    private GameObject spawner;

    private void Start()
    {
        if (amount_of_enemies >= 1)
        {
            SpawnEnemy();
        }
        if (amount_of_enemies == 0)
        {
            spawner = GameObject.FindWithTag("Spawner");
            spawner.SetActive(false);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawn_rate && amount_of_enemies >= 1)
        {
            timer -= spawn_rate;
            SpawnEnemy();
            if (amount_of_enemies == 0)
            {
                spawner = GameObject.FindWithTag("Spawner");
                spawner.SetActive(false);
            }
        }
    }


    private void SpawnEnemy()
    {
        amount_of_enemies -= 1;
        Instantiate(skeleton, transform.position, Quaternion.identity);
    }
}