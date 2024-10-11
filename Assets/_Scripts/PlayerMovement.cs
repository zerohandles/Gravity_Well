using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Blackhole")]
    [SerializeField] Transform _blackHole;
    [SerializeField] float _pullStrength;

    [Header("Player Stats")]
    [SerializeField] float _thrusterSpeed;
    [SerializeField] float _rotationSpeed;

    PlayerInput _input;
    Rigidbody2D _rb;
    InputAction _moveAction;
    bool _isMoving;
    Vector2 _direction;
    Vector2 _moveInput;
    float _escapeSpeed;

    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _moveAction = _input.actions.FindAction("Move");
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        _isMoving = _moveInput != Vector2.zero;

        HandleMovement();
        transform.up = (transform.position - _blackHole.position).normalized;
    }

    private void FixedUpdate()
    {
        _direction = (transform.position - _blackHole.position).normalized;
        _rb.AddForce(_direction * (_escapeSpeed - _pullStrength));
    }

    private void HandleMovement()
    {
        if (_isMoving)
        {
            if (_moveInput.y > 0)
                _escapeSpeed = _thrusterSpeed;
            if (_moveInput.x != 0)
            {
                _escapeSpeed *= .66f;
                transform.RotateAround(_blackHole.position, Vector3.forward, _moveInput.x * _rotationSpeed * Time.deltaTime);
            }
        }
        else
            _escapeSpeed = 0;
    }
}
