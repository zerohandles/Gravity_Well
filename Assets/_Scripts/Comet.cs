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
    
    float _mass;
    float _rotationSpeed;
    float _rotationDirection;
    float _scale;
    

    void Awake()
    {
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        _rotationDirection = Mathf.Sign(Random.Range(-1, 1));
        _scale = Random.Range(_minScale, _maxScale);
        _mass = Random.Range(_minMass, _maxMass);

        var localScale = transform.localScale;
        localScale.x *= _scale;
        localScale.y *= _scale;
        transform.localScale = localScale;
        GetComponent<Rigidbody2D>().mass = _mass;
    }

    void Update()
    {
        RotateComet();
    }

    void RotateComet()
    {
        transform.Rotate(0, 0, 1 * _rotationSpeed * _rotationDirection);
    }
}
