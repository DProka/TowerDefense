using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI uiScore;
    public TextMeshProUGUI waveNumber;
    public TextMeshProUGUI towerScore;
    public EnemySpawner enemySpawner;
    public int towerCost = 100;

    public static GameController gameController;
    
    void Start()
    {
        gameController = this;
        uiScore.text = score.ToString() + " Score";
        towerScore.text = towerCost.ToString() + " Score";
        AddWave();
    }

    void Update()
    {
        
    }

    public void AddScore(bool plus, int value)
    {
        
        if (plus == true)
        {
            score += 100;
        }
        else
        {
            if (score >= towerCost)
            {
                score -= towerCost;
                
            }
            else
            {
                score = 0;
            }
            
            
        }

        uiScore.text = score.ToString() + " Score";
    }

    public void AddWave()
    {
        waveNumber.text = enemySpawner.waveNumber.ToString() + " Wave";
    }

    public void TowerCost()
    {
        towerCost += 100;
        towerScore.text = towerCost.ToString() + " Cost";
    }
}
