using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    [SerializeField] float _minMass;
    [SerializeField] float _maxMass;
    [SerializeField] float _minRotationSpeed;
    [SerializeField] float _maxRotationSpeed;
    [SerializeField] float _minScale;
    [SerializeField] float _maxScale;
    [SerializeField] GameObject _fuelPrefab;
    [SerializeField] List<Sprite> _sprites;
    [SerializeField] AudioClip _destroySFX;
    [SerializeField] ParticleSystem _destroyParticles;

    float _health;
    float _mass;
    float _rotationSpeed;
    float _rotationDirection;
    float _scale;
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Count)];
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        _rotationDirection = Mathf.Sign(Random.Range(-1, 1));
        _scale = Random.Range(_minScale, _maxScale);
        _mass = Random.Range(_minMass, _maxMass);

        var localScale = transform.localScale;
        localScale.x *= _scale;
        localScale.y *= _scale;
        localScale.z *= _scale;
        transform.localScale = localScale;
        GetComponent<Rigidbody2D>().mass = _mass;

        _health = Mathf.Ceil(_scale * 1.5f);
    }

    void Update()
    {
        RotateComet();

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
        float amount = _scale switch
        {
            >= 1.5f => 3,
            >= 1 => 2,
            _ => 1
        };

        for (int  i = 0; i < amount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(0,.5f), Random.Range(0,.5f), 0);
            Instantiate(_fuelPrefab, transform.position + offset, Quaternion.identity);
        }
        GameManager.Instance.CometKilled();
        AudioManager.Instance.PlayOneShot(_destroySFX);
        Death();
    }

    private void Death()
    {
        Instantiate(_destroyParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void RotateComet() => transform.Rotate(0, 0, 1 * _rotationSpeed * _rotationDirection);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<PlayerHealth>(out var _player))
                _player.TakeDamage(_health);

            Death();
        }

        if (collision.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
}
