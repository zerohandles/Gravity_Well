using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Blackhole")]
    [SerializeField] Transform _blackhole;
    [SerializeField] float _pullStrength;

    [Header("Player")]
    [SerializeField] float _thrusterSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] GameObject _thrusters;
    [SerializeField] float _maxFuel;
    [SerializeField] float _boostMultiplier;
    [SerializeField] GameObject _booster;
    [SerializeField] AudioClip _thrusterSFX;
    [SerializeField] AudioClip _boostSFX;
    [SerializeField] AudioClip _upgradeSFX;

    [Header("Shooting")]
    [SerializeField] GameObject _firePoint;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _weaponCooldown;
    [SerializeField] AudioClip _bulletSFX;


    PlayerInput _input;
    Rigidbody2D _rb;
    AudioSource _audioSource;
    PlayerHealth _playerHealth;
    bool _isMoving;
    bool _isBoosting;
    bool _isOnCooldown;
    Vector2 _direction;
    Vector2 _moveInput;
    float _escapeSpeed;
    float _cooldownTimer;

    InputAction _moveAction;
    InputAction _fireAction;
    InputAction _boostAction;

    public float Fuel { get; private set; }
    public event Action OnFuelChange;

    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _playerHealth = GetComponent<PlayerHealth>();
        _moveAction = _input.actions.FindAction("Move");
        _fireAction = _input.actions.FindAction("Fire");
        _boostAction = _input.actions.FindAction("Boost");
        Fuel = _maxFuel;
    }

    void Update()
    {
        if (_playerHealth.IsDead)
            return;

        _cooldownTimer += Time.deltaTime;
        _isOnCooldown = _cooldownTimer <= _weaponCooldown;

        _moveInput = _moveAction.ReadValue<Vector2>();
        _isMoving = _moveInput != Vector2.zero;
        _isBoosting = _boostAction.ReadValue<float>() > 0 && Fuel > 0;

        if (_isOnCooldown)
        {
            _isMoving = false;
            _isBoosting = false;
        }
        
        if (_isBoosting)
        {
            Fuel -= Time.deltaTime * 20;
            Fuel = Mathf.Clamp(Fuel, 0, _maxFuel);
            OnFuelChange?.Invoke();
        }

        _thrusters.SetActive(_isMoving);
        _booster.SetActive(_isBoosting);


        HandleMovement();
        transform.up = (transform.position - _blackhole.position).normalized;

        if (_fireAction.WasPerformedThisFrame())
            HandleFiring();
    }

    void FixedUpdate()
    {
        if (_playerHealth.IsDead)
            return;

        _direction = (transform.position - _blackhole.position).normalized;
        _rb.AddForce(_direction * (_escapeSpeed - _pullStrength));
    }

    void HandleMovement()
    {
        if (_isMoving)
        {
            if (_moveInput.y > 0)
                _escapeSpeed = _thrusterSpeed;
            if (_moveInput.y < 0)
                _escapeSpeed = -_thrusterSpeed;
            if (_moveInput.x != 0)
            {
                _escapeSpeed *= .66f;
                transform.RotateAround(_blackhole.position, Vector3.forward, -_moveInput.x * _rotationSpeed * Time.deltaTime);
            }
            if (_isBoosting)
            {
                _escapeSpeed *= _boostMultiplier;
                _audioSource.clip = _boostSFX;
                if (!_audioSource.isPlaying)
                    _audioSource.Play();
            }
            else
            {
                _audioSource.clip = _thrusterSFX;
                if (!_audioSource.isPlaying)
                    _audioSource.Play();
            }
        }
        else
        {
            _escapeSpeed = 0;
            _audioSource.Stop();
        }

    }

    void HandleFiring()
    {
        _cooldownTimer = 0;
        Instantiate(_bulletPrefab, _firePoint.transform.position, transform.rotation);
        AudioManager.Instance.PlayOneShot(_bulletSFX);
    }

    public void UpgradeEngine() => _thrusterSpeed *= 1.025f;

    public void AddFuel(float amount)
    {
        Fuel += amount;
        Fuel = Mathf.Clamp(Fuel, 0, 100);
        OnFuelChange?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Upgrade"))
            AudioManager.Instance.PlayOneShot(_upgradeSFX);
    }
}
