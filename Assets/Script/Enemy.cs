using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Main")]

    public float health;
    public float speed;
    public float lifeTime;
    public CircleCollider2D enemyCollider;
    public Canvas enemyBar;
    public Slider healthBar;
    public GameObject bonusPrefab;

    [Header ("Enemy sprites")]

    public GameObject body;
    public SpriteRenderer enemyS;
    public SpriteRenderer enemyM;
    public SpriteRenderer enemyL;
    public SpriteRenderer deathSprite;

    private SpriteRenderer thisEnemySprite;

    [HideInInspector]
    public float enemyRadius = 0f;
    public int enemySize;

    private Transform target;
    private GameController gameController;
    private int pointIndex = 0;
    private EnemyStatus enemyStatus;
    private float statusTimer;
    private float baseSpeed;
    
    private void Start()
    {
        gameController = GameController.gameController;
        target = WayPoints.points[0];
        ChooseEnemy(enemySize);
        enemyRadius = enemyCollider.radius;
        baseSpeed = speed;
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    void Update()
    {
        if (health > 0)
        {
            Vector2 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector2.Distance(transform.position, target.position) <= 0.1f)
            {
                transform.position = target.position;
                GetNextWaypoint();
                ChangeAngle();
            }

            if (statusTimer > 0)
            {
                statusTimer -= Time.deltaTime;
            }
            else
            {
                statusTimer = 0;
                enemyStatus = EnemyStatus.None;
                speed = baseSpeed;
            }
        }
    }

    void ChooseEnemy(int enemy)
    {
        if (enemy == 1)
        {
            enemyS.enabled = true;
            thisEnemySprite = enemyS;
            health = 1;
        }

        if (enemy == 2)
        {
            enemyM.enabled = true;
            thisEnemySprite = enemyM;
            health = 3;
        }

        if (enemy == 3)
        {
            enemyL.enabled = true;
            thisEnemySprite = enemyL;
            health = 5;
        }

        health += gameController.waveNumber;
    }

    void GetNextWaypoint()
    {
        if (pointIndex >= WayPoints.points.Length - 1)
        {
            
            Death();
            return;
        }
        
        pointIndex++;
        target = WayPoints.points[pointIndex]; 
    }

    public void ChangeAngle()
    {
        if (transform.position.x < target.position.x)
        body.transform.rotation = Quaternion.Euler(body.transform.rotation.x, body.transform.rotation.y, 0);

        if (transform.position.x > target.position.x)
        body.transform.rotation = Quaternion.Euler(body.transform.rotation.x, body.transform.rotation.y, 180);

        if (transform.position.y < target.position.y)
        body.transform.rotation = Quaternion.Euler(body.transform.rotation.x, body.transform.rotation.y, 90);

        if (transform.position.y > target.position.y)
            body.transform.rotation = Quaternion.Euler(body.transform.rotation.x, body.transform.rotation.y, 270);
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        healthBar.value = health;
        
        if (health <= 0)
        {
            enemyBar.enabled = false;
            gameController.AddScore(100);
            Death();
        }
    }

    public void GetStatus(EnemyStatus status, float timer)
    {
        if (enemyStatus == EnemyStatus.None)
        {
            enemyStatus = status;
            statusTimer = timer;
            SetStatus();
        }
    }

    public void SetStatus()
    {
        if (enemyStatus == EnemyStatus.Freeze)
        {
            speed *= 0.3f;
        }
    }

    public void Death()
    {
        gameController.enemyArray.Remove(this);
        gameController.enemyAliveCounter--;

        if (health > 0)
        {
            gameController.UpdatePlayerHealthStatus();
            Destroy(gameObject);
        }
        else
        {
            Instantiate(bonusPrefab, transform.position, transform.rotation);
        }

        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        thisEnemySprite.enabled = false;
        deathSprite.enabled = true;

        yield return new WaitForSeconds(2);
        
        Destroy(gameObject);
    }

    
}

public enum EnemyStatus
{
    None,
    Freeze,
}
