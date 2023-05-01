using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int damage;
    public CircleCollider2D bulletCollider;
    public EnemyStatus status;
    public float statusTimer;

    [HideInInspector]
    public Enemy target;
    
    private void Update()
    {
        GetEnemy();
    }

    public void GetEnemy()
    {
        if (lifetime > 0 && target != null && target.health > 0)
        {
            lifetime -= Time.deltaTime;

            Vector2 direction = target.transform.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            float distanceToEnemy = Vector2.Distance(transform.position, target.transform.position);

            if (distanceToEnemy <= bulletCollider.radius + target.enemyRadius)
            {
                target.TakeHit(damage);
                target.GetStatus(status, statusTimer);
                Destroy();
            }
        }

        else
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        GameController.gameController.towerMissleArray.Remove(this);
        Destroy(gameObject);
    }
}
