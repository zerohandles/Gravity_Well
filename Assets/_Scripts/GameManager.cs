using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Goal> _goals = new List<Goal>();
    [SerializeField] GameObject _victoryMenu;
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

    private void Victory()
    {
        Time.timeScale = 0;
        _victoryMenu.SetActive(true);
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("You Lose!");
    }

    public void CometKilled() => _cometKills++;

    public void ShipKilled() => _shipKills++;
}
