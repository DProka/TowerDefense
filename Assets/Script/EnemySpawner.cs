using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float timeBeforSpawn;
    public float timeBetweenWaves;
    public int enemyAliveCounter = 0;
    public GameObject enemy;
    public Transform spawner;
    public int[] waveCounter;
    public static EnemySpawner enemySpawner;
    public int waveNumber = 0;

    private void Start()
    {
        enemySpawner = this;
    }

    void Update()
    {
        if (enemyAliveCounter <= 0)
        {
            if (timeBeforSpawn <= 0)
            {
                StartCoroutine(WaveSpawn());
                timeBeforSpawn = timeBetweenWaves;
            }

            timeBeforSpawn -= Time.deltaTime;
        }
    }

    IEnumerator WaveSpawn()
    {

        for (int i = 0; i < waveCounter[waveNumber]; i++)
        {
            EnemySpawn();

            yield return new WaitForSeconds(1f);
        }

        waveNumber++;
        GameController.gameController.AddWave();
    }

    void EnemySpawn()
    {
        Instantiate(enemy, spawner.position, spawner.rotation);
        enemyAliveCounter++;
    }
}
