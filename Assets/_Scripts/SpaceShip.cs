using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] float _minMass;
    [SerializeField] float _maxMass;
    [SerializeField] float _minScale;
    [SerializeField] float _maxScale;
    [SerializeField] GameObject[] _upgradePrefabs;
    [SerializeField] List<Sprite> _sprites;

    GameObject _blackhole;
    SpriteRenderer _spriteRenderer;
    float _health;
    float _mass;
    float _scale;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Count)];
        _blackhole = GameObject.Find("Blackhole");

        _scale = Random.Range(_minScale, _maxScale);
        _mass = Random.Range(_minMass, _maxMass);
        _health = Mathf.Ceil(2 * _scale);

        var localScale = transform.localScale;
        localScale.x *= _scale;
        localScale.y *= _scale;
        localScale.z *= _scale;
        transform.localScale = localScale;
        GetComponent<Rigidbody2D>().mass = _mass;
    }

    void Update()
    {
        if (_health <= 0)
        {
            SpawnLoot();
            Death();
        }
        transform.up = transform. position - _blackhole.transform.position;

        if (Vector2.Distance(transform.position, Vector2.zero) < 0.1f)
            Destroy(gameObject);
    }

    private void TakeDamage()
    {
        _health--;

        if (_health <= 0)
            SpawnLoot();
    }

    private void SpawnLoot()
    {
        bool isBig = _scale >= 1.3f;
        var loot = GenerateLoot();
        Instantiate(loot, transform.position, Quaternion.identity);

        if (isBig)
        {
            loot = GenerateLoot();
            Instantiate(loot, transform.position, Quaternion.identity);
        }

        Death();
    }

    GameObject GenerateLoot()
    {
        int rand = (int)Mathf.Round(Random.Range(0, _upgradePrefabs.Length));

        return _upgradePrefabs[rand];
    }

    void Death()
    {
        // Particle effects
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            TakeDamage();

        if (collision.CompareTag("Player"))
        {
            var player = collision.transform.GetComponent<PlayerHealth>();
            player.TakeDamage(Mathf.Ceil(_scale * 2));
            Death();
        }
    }
}
