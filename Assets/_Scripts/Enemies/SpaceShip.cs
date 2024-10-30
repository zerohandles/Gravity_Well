using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [Header("Ship Stats")]
    [SerializeField] float _minMass;
    [SerializeField] float _maxMass;
    [SerializeField] float _minScale;
    [SerializeField] float _maxScale;
    [SerializeField] List<Sprite> _sprites;

    [Header("Loot")]
    [SerializeField] GameObject[] _upgradePrefabs;

    [Header("Death Effects")]
    [SerializeField] AudioClip _destroySFX;
    [SerializeField] ParticleSystem _destroyParticles;

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

        // Randomize ships scale and mass
        var localScale = transform.localScale;
        localScale.x *= _scale;
        localScale.y *= _scale;
        localScale.z *= _scale;
        transform.localScale = localScale;
        GetComponent<Rigidbody2D>().mass = _mass;
    }

    void Update()
    {
        transform.up = transform. position - _blackhole.transform.position;

        // Destroy ship when it reaches the center of the blackhole
        if (Vector2.Distance(transform.position, Vector2.zero) < 0.1f)
            Destroy(gameObject);
    }

    // Reduce ship's health
    private void TakeDamage()
    {
        _health--;

        if (_health <= 0)
            SpawnLoot();
    }

    // Spawn loot if destroyed by the player
    private void SpawnLoot()
    {
        bool isBig = _scale >= 1.3f;
        var loot = GenerateLoot();
        Vector3 offset = new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f), 0);

        // Spawn loot with an offset to avoid loot stacking up
        Instantiate(loot, transform.position + offset, Quaternion.identity);

        // Increase amount of loot based on the size/health of the ship
        if (isBig)
        {
            loot = GenerateLoot();
            offset = new Vector3(Random.Range(0, .5f), Random.Range(0, 0.5f), 0);
            Instantiate(loot, transform.position + offset, Quaternion.identity);
        }
        GameManager.Instance.ShipKilled();
        AudioManager.Instance.PlayOneShot(_destroySFX);
        Death();
    }

    // Return a random loot prefab
    GameObject GenerateLoot()
    {
        int rand = Random.Range(0, _upgradePrefabs.Length);

        return _upgradePrefabs[rand];
    }

    // Spawn death effects and destroy the game object
    void Death()
    {
        Instantiate(_destroyParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            TakeDamage();

        // If ship collides with the player, down't spawn loot and damage the player
        if (collision.CompareTag("Player"))
        {
            var player = collision.transform.GetComponent<PlayerHealth>();
            player.TakeDamage(Mathf.Ceil(_scale * 2));
            Death();
        }
    }
}
