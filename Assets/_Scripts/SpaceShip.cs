using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] float _minMass;
    [SerializeField] float _maxMass;
    [SerializeField] float _minScale;
    [SerializeField] float _maxScale;
    [SerializeField] GameObject _upgradePrefab;
    float _health;

    float _mass;
    float _scale;

    void Awake()
    {
        _scale = Random.Range(_minScale, _maxScale);
        _mass = Random.Range(_minMass, _maxMass);
        _health = Mathf.Ceil(2 * _scale);

        var localScale = transform.localScale;
        localScale.x *= _scale;
        localScale.y *= _scale;
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
    }

    private void SpawnLoot()
    {
        // Drop upgrades for player
    }

    void Death()
    {
        // Drop upgrades for player
        // Particle effects
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            _health--;

        if (collision.CompareTag("Player"))
        {
            Death();
        }
    }
}
