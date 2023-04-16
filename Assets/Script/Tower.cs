using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]

    public float damage;
    public float attackColdown;
    public float range = 3f;
    public float rotationSpeed = 10f;
    public float updateTargetColdown = 0.5f;


    protected Enemy target;
    protected float fireTimer;
    protected float updateTimer;


    [Header("Used Objects")]

    public Transform partToRotate;
    public Transform firePoint;
    public Transform towerCenter;

    protected virtual void Start()
    {
        fireTimer = attackColdown;
        updateTimer = updateTargetColdown;
    }

    void Update()
    {
        UpdateTarget();
        RotateTower();
        UpdateTower();
    }

    protected virtual void UpdateTower()
    {
        
    }

    void UpdateTarget()
    {
        updateTimer -= Time.deltaTime;

        if (updateTimer <= 0)
        {
            updateTimer = updateTargetColdown;
            target = GameController.gameController.GetNearestEnemy(towerCenter.position);

            if (target == null)
            {
                return;
            }

            float distanceToTarget = Vector2.Distance(towerCenter.position, target.transform.position);

            if (distanceToTarget > range)
            {
                target = null;
            }
        }
    }

    void RotateTower()
    {
        if (target == null)
        {
            return;
        }

        Vector2 dir = target.transform.position - towerCenter.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, rotation, rotationSpeed * Time.deltaTime);
    }



    private void OnDrawGizmosSelected()
    {
        if (towerCenter == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(towerCenter.position, range);
    }
}
