using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _maxHealth;
    [SerializeField] Transform _blackhole;

    public float Health { get; private set; }
    float _minDistance = 1.1f;

    public event Action OnHealthChange;

    void Awake() => Health = _maxHealth;

    void Update()
    {
        if (Vector2.Distance(transform.position, _blackhole.position) <= _minDistance)
            Death();
    }

    void Death()
    {
        Debug.Log("Game Over");
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        OnHealthChange?.Invoke();

        if (Health <= 0)
            Death();
    }

    public void GainHealth(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, _maxHealth);
        OnHealthChange?.Invoke();
    }
}
