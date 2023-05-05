using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLaser : Tower
{
    [Header("TowerLaser Attributes")]

    public float attackTime;
    private float attackTimer;

    public LineRenderer lineRenderer;

    protected override void Start()
    {
        base.Start();
        attackTimer = attackTime;

    }

    protected override void UpdateTower()
    {
        if (target == null || target.health <= 0)
        {
            lineRenderer.enabled = false;
            return;
        }
        
        fireTimer -= Time.deltaTime;
        
        if (fireTimer <= 0 && target.health > 0)
        {
            target.TakeHit(damage * Time.deltaTime);
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, target.transform.position);

            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                lineRenderer.enabled = false;
                fireTimer = attackColdown;
                attackTimer = attackTime;
            }
        }
    }

}
