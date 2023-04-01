using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float lifeTime;
    public GameObject body;
    
    private Transform target;
    private EnemySpawner enemySpawner;
    private GameController gameController;
    private int pointIndex = 0;


    private void Start()
    {
        enemySpawner = EnemySpawner.enemySpawner;
        gameController = GameController.gameController;
        target = WayPoints.points[0];
        health += enemySpawner.waveNumber;
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

    public void TakeHit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        enemySpawner.enemyAliveCounter--;
        gameController.AddScore(true, 100);
        
    }
}
