using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;

    void Start() => Invoke(nameof(DestroyBullet), _lifeTime);

    void Update() => transform.Translate(_speed * Time.deltaTime * Vector2.up);

    void DestroyBullet() => Destroy(gameObject);

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Debris"))
        {
            //Instantiate explosion effect
            Destroy(gameObject);
        }
    }
}
