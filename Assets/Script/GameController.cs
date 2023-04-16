using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [Header("Game Controller")]
    
    public int score;
    public TextMeshProUGUI uiScore;
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI towerScore;
    public int towerCost = 100;
    public static GameController gameController;

    [Header("EnemySpawn Controller")]

    public GameObject enemySPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemyLPrefab;
    public Transform spawner;
    public float timeBeforSpawn;
    public float timeBetweenWaves;
    public float timeBetweenEnemyspawn;
    public int waveNumber = 0;
    public int enemyAliveCounter = 0;
    public int[] waveCounter;
    public List<Enemy> enemyArray = new List<Enemy>();

    private float waveTimer;

    public List<Bullet> towerMissleArray = new List<Bullet>();

    void Start()
    {
        gameController = this;
        uiScore.text = score.ToString() + " Score";
        towerScore.text = towerCost.ToString() + " Cost";
        waveTimer = timeBetweenWaves;
    }
    void Update()
    {

        if (enemyArray.Count == 0)
        {
            if (waveTimer > 0)
            {
                waveTimer -= Time.deltaTime;
            }

            if (waveTimer <= 0)
            {
                if (waveNumber >= waveCounter.Length - 1)
                    waveNumber = 10;

                StartCoroutine(WaveSpawn());
                waveTimer = timeBetweenWaves;
            }
        }
    }

    IEnumerator WaveSpawn()
    {
        for (int i = 0; i < waveCounter[waveNumber]; i++)
        {
            if(i <= 10)
            {
                EnemySpawn(enemySPrefab);
            }
            
            if(i > 10 && i < 21)
            {
                EnemySpawn(enemyMPrefab);
            }
            
            if(i >= 21)
            {
                EnemySpawn(enemyLPrefab);
            }
            
            yield return new WaitForSeconds(timeBetweenEnemyspawn);
        }

        AddWave();
    }

    void EnemySpawn(GameObject enemy)
    {
        Enemy g = Instantiate(enemy, spawner.position, spawner.rotation).GetComponent<Enemy>();
        enemyArray.Add(g);
        enemyAliveCounter++;
    }


    public void AddScore(int value)
    {
        score += value;
        uiScore.text = score.ToString() + " Score";
    }

    public void AddWave()
    {
       
     
        waveNumber++;
        waveNumberText.text = waveNumber.ToString() + " Wave";
    }

    public void TowerCost()
    {
        towerCost += 100;
        towerScore.text = towerCost.ToString() + " Cost";
    }

    public Enemy GetNearestEnemy(Vector2 point)
    {
        float shortDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;
        foreach (Enemy enemy in enemyArray)
        {
            float distanceToEnemy = Vector2.Distance(point, enemy.transform.position);
            if (distanceToEnemy < shortDistance)
            {
                shortDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}
