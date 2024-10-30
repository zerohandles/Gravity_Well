using System.Collections;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _countdownText; 
    float _countdownTime = 3.0f;

    void Start() => StartCoroutine(StartCountdown());

    // Pause time and display a countdown timer before resuming the game
    IEnumerator StartCountdown()
    {
        Time.timeScale = 0;

        while (_countdownTime > 0)
        {
            _countdownText.text = _countdownTime.ToString("F0"); 
            yield return new WaitForSecondsRealtime(1.0f); 
            _countdownTime -= 1.0f;
        }

        // Wait an extra second to display 0
        _countdownText.text = "0";
        yield return new WaitForSecondsRealtime(1.0f); 

        Time.timeScale = 1;
        _countdownText.text = ""; 
        _countdownText.gameObject.SetActive(false);
    }
}

