using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public SpriteRenderer rend;
    public GameObject towerGunPrefab;
    public GameObject towerLaserPrefab;
    public GameObject towerIcePrefab;

    private Color startColor;
    private bool active;
    private bool employed;
    

    void Start()
    {
        startColor = rend.color;
        active = false;
        employed = false;
    }
    private void Update()
    {
        if (active == true && Input.GetMouseButtonDown(0))
        {
            TowerCreation(towerGunPrefab);
        }
        
        if (active == true && Input.GetMouseButtonDown(1))
        {
            TowerCreation(towerLaserPrefab);
        }
        
        if (active == true && Input.GetMouseButtonDown(2))
        {
            TowerCreation(towerIcePrefab);
        }
    }

    void TowerCreation(GameObject tower)
    {
        int towerCost = GameController.gameController.towerCost;
        int pScore = GameController.gameController.score;

        if (pScore >= towerCost && !employed)
        {
            Instantiate(tower, transform.position, transform.rotation);
            GameController.gameController.AddScore(-towerCost);
            GameController.gameController.UpdateTowerCost();
            employed = true;
        }
        
    }

    private void OnMouseEnter()
    {
        rend.color = hoverColor;
        active = true;

    }

    private void OnMouseExit()
    {
        rend.color = startColor;
        active = false;
    }

    
}
