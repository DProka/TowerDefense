using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Attributes")]

    public int damage;
    public float attackSpeed;
    public float range = 3f;
    public float rotationSpeed = 10f;
    public string enemyTag = "Enemy";

    private float fireCooldown;
    private Transform target;

    [Header("Used Objects")]

    public Transform partToRotate;
    public GameObject bullet;
    public Transform firePoint;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortDistance)
            {
                shortDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortDistance <= range)
        {
            target = nearestEnemy.transform;
        }

        else
        {
            target = null;
        }
    }

    void Update()
    {
        fireCooldown += Time.deltaTime;

        if (target == null)
        {
            return;
        }

        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, rotation, rotationSpeed * Time.deltaTime);

        if(fireCooldown >= attackSpeed)
        {
            Shoot();
            fireCooldown = 0;
        }

        
    }

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, partToRotate.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
        
    }
}
