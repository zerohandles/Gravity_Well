using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] SceneField _mainMenuScene;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] AudioClip _pauseSound;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    // Reload the gameplay scene
    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load the main menu scene
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_mainMenuScene);
    }

    // Toggle the pause screen and timescale
    public void Pause()
    {
        AudioManager.Instance.PlayOneShot(_pauseSound);
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
        Time.timeScale = _pauseMenu.activeSelf ? 0.0f : 1.0f;
    }
}
