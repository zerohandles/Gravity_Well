using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] SceneField _mainMenuScene;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] AudioClip _pauseSound;

    PlayerInput _playerInput;
    InputAction _pauseAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _pauseAction = _playerInput.actions.FindAction("Pause");
    }

    private void Update()
    {
        if (_pauseAction != null && _pauseAction.WasPerformedThisFrame())
            Pause();
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_mainMenuScene);
    }

    public void Pause()
    {
        AudioManager.Instance.PlayOneShot(_pauseSound);
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        Time.timeScale = _pauseMenu.activeSelf ? 0.0f : 1.0f;
    }
}
