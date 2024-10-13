using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _tutorialMenu;
    [SerializeField] Scrollbar _tutorialScroll;
    [SerializeField] GameObject _creditsMenu;
    [SerializeField] Scrollbar _creditsScroll;
    [SerializeField] AudioClip _buttonSound;
    [SerializeField] OpeningCinamatics _openingCinamatics;

    public void StartGame()
    {
        AudioManager.Instance.PlayOneShot(_buttonSound);
        _openingCinamatics.StartCinematic();
    }

    public void ToggleTutorial()
    {
        AudioManager.Instance.PlayOneShot(_buttonSound);
        _tutorialScroll.value = 1;
        _tutorialMenu.SetActive(!_tutorialMenu.activeSelf);
        _tutorialScroll.value = 1;
    }

    public void ToggleCredits()
    {
        AudioManager.Instance.PlayOneShot(_buttonSound);
        _creditsScroll.value = 1;
        _creditsMenu.SetActive(!_creditsMenu.activeSelf);
        _creditsScroll.value = 1;
    }
}
