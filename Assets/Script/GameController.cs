using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;


public class GameController : MonoBehaviour
{
    [Header("Game Controller")]
    
    public int score;
    public int towerCost = 100;
    public int playerHealth = 3;
    public static GameController gameController;

    [Header("UI")]

    public Image life1; 
    public Image life2; 
    public Image life3;
    public TextMeshProUGUI uiScore;
    public TextMeshProUGUI towerPrice;
    public GameObject failScreen;
    public GameObject victoryScreen;
    public Button restartButton;
    public Button menuButton;

    [Header("EnemySpawn Controller")]

    public GameObject enemyPrefab;
    public Transform spawner;
    public float timeBeforSpawn;
    public float timeBetweenWaves;
    public float timeBetweenEnemyspawn;
    public int waveNumber = 0;
    public int[] waveCounter;

    [HideInInspector]
    public bool gameIsActive;
    public int enemyAliveCounter = 0;
    public List<Enemy> enemyArray = new List<Enemy>();
    public List<Bullet> towerMissleArray = new List<Bullet>();
    public event Action<int> OnWaveChange;

    private float waveTimer;

    void Awake()
    {
        gameIsActive = true;
        gameController = this;
        failScreen.SetActive(false);
        uiScore.text = score.ToString() + " Score";
        towerPrice.text = towerCost.ToString() + " Cost";
        waveTimer = timeBetweenWaves;
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMenu);
    }

    void Update()
    {
        if (enemyArray.Count <= 0 && gameIsActive)
        {
            if (waveTimer > 0)
            {
                waveTimer -= Time.deltaTime;
            }

            if (waveTimer <= 0)
            {
                AddWave();
               
                if (waveNumber < waveCounter.Length)
                {
                    StartCoroutine(WaveSpawn());
                    waveTimer = timeBetweenWaves;
                }
                else
                {
                    waveNumber = 10;
                    victoryScreen.SetActive(true);
                    gameIsActive = false;
                }
            }
        }
    }

    IEnumerator WaveSpawn()
    {
        for (int i = 0; i < waveCounter[waveNumber]; i++)
        {
            if(i <= 10)
            {
                EnemySpawn(1);
            }
            
            if(i > 10 && i < 21)
            {
                EnemySpawn(2);
            }
            
            if(i >= 21)
            {
                EnemySpawn(3);
            }
            
            yield return new WaitForSeconds(timeBetweenEnemyspawn);
        }
        waveNumber++;
        //AddWave();
    }

    void EnemySpawn(int enemySize)
    {
        GameObject e = Instantiate(enemyPrefab, spawner.position, spawner.rotation);
        Enemy g = e.GetComponent<Enemy>();
        g.enemySize = enemySize;
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
        if (OnWaveChange != null)
            OnWaveChange.Invoke(waveNumber);
           // OnWaveChange?.Invoke(waveNumber);
    }

    public void UpdateTowerCost()
    {
        towerCost += 100;
        towerPrice.text = towerCost.ToString() + " Cost";
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

    public void UpdatePlayerHealthStatus()
    {
        if (playerHealth > 0)
            gameController.playerHealth -= 1;

        if (playerHealth == 2)
            life3.enabled = false;
        
        if(playerHealth == 1)
            life2.enabled = false;
        
        if(playerHealth == 0)
        {
            failScreen.SetActive(true);
            gameIsActive = false;
            life1.enabled = false;
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    } 
}
