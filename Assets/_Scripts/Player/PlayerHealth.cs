using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float _maxHealth;

    [Header("Damage Effects")]
    [SerializeField] AudioClip _damageSFX;
    [SerializeField] AudioClip _deathSFX;
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] GameObject _engines;

    [Header("Blackhole")]
    [SerializeField] Transform _blackhole;

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
        // If the player gets too close the the blackhole they die
        if (Vector2.Distance(transform.position, _blackhole.position) <= _minDistanceToBlackhole)
            Death();
    }

    // Trigger game over and play death effects
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

    // Reduce player's health by amount and play damage effects
    public void TakeDamage(float amount)
    {
        Health -= amount;
        OnHealthChange?.Invoke();
        AudioManager.Instance.PlayOneShot(_damageSFX);
        _shake.StartShaking();

        // Trigger death if player has 0 health
        if (Health <= 0)
            Death();
    }

    // Increase player's health by amount
    public void GainHealth(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, _maxHealth);
        OnHealthChange?.Invoke();
    }

    // Slight delay before invoking the death event to trigger the game over screen
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2);
        GetComponent<AudioSource>().Stop();
        TriggerDeath?.Invoke();
    }
}
