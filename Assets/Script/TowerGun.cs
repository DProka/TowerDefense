using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGun : Tower
{
    [Header("TowerGun Attributes")]
    public float attackSpeed;

    private float fireCooldown;

    public GameObject bullet;

    protected override void Start()
    {
        updateTimer = updateTargetColdown;
    }

    protected override void UpdateTower()
    {
        if (target == null) return;

        fireCooldown += Time.deltaTime;

        if (fireCooldown >= attackSpeed && target.health > 0)
        {
            Shoot();
            fireCooldown = 0;
        }
    }

    void Shoot()
    {
        Bullet bulletS = Instantiate(bullet, firePoint.position, partToRotate.rotation).GetComponent<Bullet>();
        GameController.gameController.towerMissleArray.Add(bulletS);
        bulletS.target = target;
    }
}
