using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(new Vector2(transform.position.x * speed * Time.deltaTime, 0));
    }
}
