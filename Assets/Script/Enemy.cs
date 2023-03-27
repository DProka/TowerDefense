using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject wayoint;
    public GameObject body;
    public float lifeTime;

    private float enemyX;
    private float enemyY;
    private float enemySpeed;

    void Update()
    {
        lifeTime += Time.deltaTime;
        Movement();

        if (health <= 0 || transform.position.x < -12)
        {
            Death();
        }
    }

    public void ChangeAngle()
    {

    }

    public void Movement()
    {
        enemyX = transform.position.x;
        enemyY = transform.position.y;
        enemySpeed = speed * Time.deltaTime;

        if (enemyX < 5 && enemyY > -4)
        {
            transform.position = new Vector2(transform.position.x + enemySpeed, transform.position.y);
        }
        else if (enemyX >=5 && enemyY > -4)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - enemySpeed);
            body.transform.rotation = Quaternion.Euler(body.transform.rotation.x, body.transform.rotation.y, -90);
        }
        else if (enemyY <= -4 && enemyX > -13)
        {
            transform.position = new Vector2(transform.position.x - enemySpeed, transform.position.y);
            body.transform.rotation = Quaternion.Euler(body.transform.rotation.x, body.transform.rotation.y, -180);
        }

    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
