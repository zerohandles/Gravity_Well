using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Goal> _goals = new List<Goal>();
    [SerializeField] GameObject _victoryMenu;
    [SerializeField] GameObject _defeatMenu;
    [SerializeField] TextMeshProUGUI _cometText;
    [SerializeField] TextMeshProUGUI _shipText;
    [SerializeField] TextMeshProUGUI _timeText;
    [SerializeField] TextMeshProUGUI _scoreText;
    float _targetTime = 300;
    float _startTime;
    float _cometKills;
    float _shipKills;

    PlayerHealth _player;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _player = GameObject.Find("Player").GetComponent<PlayerHealth>();    
    }

    void OnEnable()
    {
        foreach (Goal goal in _goals)
            goal.TriggerVictory += Victory;
        _player.TriggerDeath += GameOver;
    }

    private void OnDisable()
    {
        foreach (Goal goal in _goals)
            goal.TriggerVictory -= Victory;
        _player.TriggerDeath -= GameOver;
    }

    void Start()
    {
        _startTime = Time.time;    
    }

    private void Victory()
    {
        Time.timeScale = 0;
        float elapsedTime = Time.time - _startTime;
        float timeMultiplier = _targetTime - elapsedTime;
        int timeScore = (int)timeMultiplier * 100;

        _cometText.text = $"Comets Destroyed: {_cometKills}";
        _shipText.text = $"Ships Destroyed: {_shipKills}";
        _timeText.text = $"Time Bonus: {timeScore}";
        _scoreText.text = ((_cometKills * 368) + (_shipKills * 510) + timeScore).ToString("N0");
        _victoryMenu.SetActive(true);
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        _defeatMenu.SetActive(true);
    }

    public void CometKilled() => _cometKills++;

    public void ShipKilled() => _shipKills++;
}
