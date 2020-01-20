using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeleton;
    public int amount_of_enemies = 1;

    IEnumerator WaitTime()
    {
        yield return new WaitForSecondsRealtime(5);
    }

    void Start()
    {
        for (int i = 0; amount_of_enemies > i; amount_of_enemies--)
        {
            SpawnRandomPrefab();
            StartCoroutine(WaitTime());
        }
    }


    private void SpawnRandomPrefab()
    {
        Instantiate(skeleton, transform.position, Quaternion.identity, transform);
    }
}