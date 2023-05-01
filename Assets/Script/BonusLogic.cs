using UnityEngine;

public class BonusLogic : MonoBehaviour
{
    public float speed;

    private Vector3 flyPoint = new Vector2(15,7);

    void Update()
    {
        Vector2 dir = flyPoint - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        float distance = Vector2.Distance(transform.position, flyPoint);

        if (distance <= 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
