using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SceneField _mainGameScene;
    [SerializeField] GameObject _tutorialMenu;
    [SerializeField] Scrollbar _tutorialScroll;
    [SerializeField] GameObject _creditsMenu;
    [SerializeField] Scrollbar _creditsScroll;

    public void StartGame()
    {
        SceneManager.LoadScene(_mainGameScene);
    }

    public void ToggleTutorial()
    {
        _tutorialScroll.value = 1;
        _tutorialMenu.SetActive(!_tutorialMenu.activeSelf);
        _tutorialScroll.value = 1;
    }

    public void ToggleCredits()
    {
        _creditsScroll.value = 1;
        _creditsMenu.SetActive(!_creditsMenu.activeSelf);
        _creditsScroll.value = 1;
    }
}
