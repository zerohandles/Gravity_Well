
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] TextMeshProUGUI _timerText;

    PlayerHealth _player;
    float _time;

    void OnEnable() => _player.OnHealthChange += UpdateHealth;

    void OnDisable() => _player.OnHealthChange -= UpdateHealth;

    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void Start()
    {
        _healthBar.maxValue = _player.Health;
        _healthBar.value = _player.Health;
    }

    void Update()
    {
        _time += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(_time);

        _timerText.text = string.Format("{0:D1}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
    }

    void UpdateHealth()
    {
        _healthBar.value = _player.Health;
    }
}
