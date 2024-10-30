using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject _tutorialMenu;
    [SerializeField] Scrollbar _tutorialScroll;
    [SerializeField] GameObject _creditsMenu;
    [SerializeField] Scrollbar _creditsScroll;

    [Header("Audio")]
    [SerializeField] AudioClip _buttonSound;

    [Header("Cinematic")]
    [SerializeField] OpeningCinamatics _openingCinamatics;

    public void StartGame()
    {
        AudioManager.Instance.PlayOneShot(_buttonSound);
        _openingCinamatics.StartCinematic();
    }

    // Toggle the tutorial screen display
    public void ToggleTutorial()
    {
        AudioManager.Instance.PlayOneShot(_buttonSound);
        _tutorialScroll.value = 1;
        _tutorialMenu.SetActive(!_tutorialMenu.activeSelf);
        _tutorialScroll.value = 1;
    }

    // Toggle the credits screen display
    public void ToggleCredits()
    {
        AudioManager.Instance.PlayOneShot(_buttonSound);
        _creditsScroll.value = 1;
        _creditsMenu.SetActive(!_creditsMenu.activeSelf);
        _creditsScroll.value = 1;
    }
}
