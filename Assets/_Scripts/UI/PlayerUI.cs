
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] Slider _fuelBar;
    [SerializeField] TextMeshProUGUI _timerText;

    PlayerHealth _player;
    PlayerMovement _playerFuel;
    float _time;

    void OnEnable()
    {
        _player.OnHealthChange += UpdateHealth;
        _playerFuel.OnFuelChange += UpdateFuel;
    }

    void OnDisable()
    {
        _player.OnHealthChange -= UpdateHealth;
        _playerFuel.OnFuelChange -= UpdateFuel;
    }

    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _playerFuel = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        _healthBar.maxValue = _player.Health;
        _healthBar.value = _player.Health;
        _fuelBar.maxValue = _playerFuel.Fuel;
        _fuelBar.value = _playerFuel.Fuel;
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

    private void UpdateFuel()
    {
        _fuelBar.value = _playerFuel.Fuel;
    }
}
