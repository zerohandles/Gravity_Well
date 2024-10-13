using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _maxHealth;
    [SerializeField] Transform _blackhole;
    [SerializeField] AudioClip _damageSFX;
    [SerializeField] AudioClip _deathSFX;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] GameObject _engines;
    SpriteRenderer _spriteRenderer;
    CameraShake _shake;

    public float Health { get; private set; }
    public bool IsDead { get; private set; }
    float _minDistanceToBlackhole = 1.1f;

    public event Action OnHealthChange;
    public event Action TriggerDeath;

    void Awake()
    {
        Health = _maxHealth;
        _shake = Camera.main.GetComponent<CameraShake>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, _blackhole.position) <= _minDistanceToBlackhole)
            Death();
    }

    void Death()
    {
        if (IsDead)
            return;
        IsDead = true;
        StartCoroutine(DeathDelay());
        Instantiate(_deathParticles, transform.position, Quaternion.identity);
        _spriteRenderer.enabled = false;
        _engines.SetActive(false);
        AudioManager.Instance.PlayOneShot(_deathSFX);
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        OnHealthChange?.Invoke();
        AudioManager.Instance.PlayOneShot(_damageSFX);
        _shake.StartShaking();

        if (Health <= 0)
            Death();
    }

    public void GainHealth(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, _maxHealth);
        OnHealthChange?.Invoke();
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2);
        GetComponent<AudioSource>().Stop();
        TriggerDeath?.Invoke();
    }
}
