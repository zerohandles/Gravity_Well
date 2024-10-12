using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PowerUp : MonoBehaviour
{
    void Update()
    {
        if (Vector2.Distance(transform.position, Vector2.zero) < 0.1f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            UsePowerUp(collision.gameObject);
    }

    public virtual void UsePowerUp(GameObject gameObject)
    {
        
    }
}
