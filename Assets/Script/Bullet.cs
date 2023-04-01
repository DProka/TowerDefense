using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;
    public string collisionTag;
   

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        
        if (lifetime <= 0)
        {
            Destroy();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeHit(damage);
        }
        Destroy();
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
