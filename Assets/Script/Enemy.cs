using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public float lifeTime;
    public GameObject body;
    public CircleCollider2D enemyCollider;
    public float enemyRadius = 0f;

    private Transform target;
    private GameController gameController;
    private int pointIndex = 0;
    private string enemyStatus;
    private float statusTimer;
    private float baseSpeed;
    
    private void Start()
    {
        gameController = GameController.gameController;
        target = WayPoints.points[0];
        enemyRadius = enemyCollider.radius;
        health += gameController.waveNumber; // * gameController.waveNumber;
        baseSpeed = speed;
    }

    void Update()
    {
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector2.Distance(transform.position, target.position) <= 0.1f)
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
            enemyStatus = "None";
            speed = baseSpeed;
        } 
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
        
        if (health <= 0)
        {
            Death();
        }
    }

    public void GetStatus(string status, float timer)
    {
        if (enemyStatus == "None")
        {
            enemyStatus = status;
            statusTimer = timer;
            SetStatus();
        }
    }

    public void SetStatus()
    {
        if (enemyStatus == "Freeze")
        {
            speed *= 0.3f;
        }
    }

    public void Death()
    {
        if (health <= 0)
        {
            gameController.AddScore(100);
        }

        gameController.enemyArray.Remove(this);
        gameController.enemyAliveCounter--;
        Destroy(gameObject);
    }
}
