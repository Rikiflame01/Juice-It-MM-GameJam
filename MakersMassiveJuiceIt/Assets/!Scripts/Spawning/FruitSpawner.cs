using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruits;

    public Transform[] spawnPositions;

    public int spawnNumber = 5;

    private void Awake()
    {
        InvokeRepeating("fruitSpawn", 5, 5);
    }

    private void fruitSpawn()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject randomGameObject = fruits[Random.Range(0, fruits.Length)];
            Transform randomPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
            Instantiate(randomGameObject, randomPosition.position, Quaternion.identity);
        }
    }
}
