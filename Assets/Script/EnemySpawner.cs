using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float timeToSpawn;
    public GameObject enemy;

    private float spawn;

    void Start()
    {
        
    }

    void Update()
    {
        spawn += Time.deltaTime;

        if (spawn >= timeToSpawn)
        {
            EnemySpawn();
        }

    }

    public void EnemySpawn()
    {
        Instantiate(enemy, new Vector2(-10, 2), Quaternion.Euler(0,0,0));
        spawn = 0;
    }
}
