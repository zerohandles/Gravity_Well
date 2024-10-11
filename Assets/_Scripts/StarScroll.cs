using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScroll : MonoBehaviour
{
    [SerializeField] Transform _blackHole;
    [SerializeField] Transform _player;
    [SerializeField] float _scrollSpeed;
    Vector2 _direction;

    void Update()
    {
        _direction = (_blackHole.position - _player.position).normalized;
        transform.Translate(_scrollSpeed * Time.deltaTime * _direction);
    }
}
