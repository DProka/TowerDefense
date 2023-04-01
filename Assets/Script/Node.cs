using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public SpriteRenderer rend;
    public GameObject tower;

    private Color startColor;
    private bool active;
    

    void Start()
    {
        startColor = rend.color;
        active = false;   
    }
    private void Update()
    {
        if (active == true && Input.GetMouseButtonDown(0))
        {
            TowerCreation();
        }
    }

    void TowerCreation()
    {
        int towerCost = GameController.gameController.towerCost;
        int pScore = GameController.gameController.score;

        if (pScore >= towerCost)
        {
            Instantiate(tower, transform.position, transform.rotation);
            GameController.gameController.AddScore(false, towerCost);
            GameController.gameController.TowerCost();
            
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
