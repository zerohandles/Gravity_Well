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
        Vector3 offset = new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f), 0);

        Instantiate(loot, transform.position + offset, Quaternion.identity);

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

    GameObject GenerateLoot()
    {
        int rand = Random.Range(0, _upgradePrefabs.Length);

        return _upgradePrefabs[rand];
    }

    void Death()
    {
        Instantiate(_destroyParticles, transform.position, Quaternion.identity);
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
