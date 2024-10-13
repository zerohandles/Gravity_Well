using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningCinamatics : MonoBehaviour
{
    [SerializeField] TextAsset _openingScript;
    [SerializeField] GameObject _uiContainer;
    [SerializeField] CameraShake _cameraShake;
    [SerializeField] AudioClip _rumbleAudio;
    [SerializeField] SceneField _mainGameScene;
    bool _cinematicStarted = false;

    private void OnEnable() => DialogManager.Instance.RumbleEvent += StartRumble;

    private void OnDisable() => DialogManager.Instance.RumbleEvent -= StartRumble;

    void Update()
    {
        if (_cinematicStarted && !DialogManager.Instance.DialogIsPlaying)
        {
            SceneManager.LoadScene(_mainGameScene);
        }
    }

    private void StartRumble()
    {
        AudioManager.Instance.PlayOneShot(_rumbleAudio);
        _cameraShake.isShaking = true;
    }

    public void StartCinematic()
    {
        _uiContainer.SetActive(false);
        StartCoroutine(CinematicBuffer());
    }

    IEnumerator CinematicBuffer()
    {
        yield return new WaitForSeconds(2);
        DialogManager.Instance.EnterDialongMode(_openingScript);
        yield return new WaitForSeconds(1);
        _cinematicStarted = true;
    }
}
