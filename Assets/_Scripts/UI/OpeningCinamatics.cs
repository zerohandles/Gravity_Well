using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningCinamatics : MonoBehaviour
{
    [SerializeField] TextAsset _openingScript;
    [SerializeField] GameObject _uiContainer;

    [Header("Camera")]
    [SerializeField] CameraShake _cameraShake;
    [SerializeField] AudioClip _rumbleAudio;

    [Header("Scene to Load")]
    [SerializeField] SceneField _mainGameScene;
    bool _cinematicStarted = false;

    void OnEnable() => DialogManager.Instance.RumbleEvent += StartRumble;

    void OnDisable() => DialogManager.Instance.RumbleEvent -= StartRumble;

    void Update()
    {
        // Load the gameplay scene after the opening cutscene finishes
        if (_cinematicStarted && !DialogManager.Instance.DialogIsPlaying)
            SceneManager.LoadScene(_mainGameScene);
    }

    // Start camera shake effect
    void StartRumble()
    {
        AudioManager.Instance.PlayOneShot(_rumbleAudio);
        _cameraShake.isShaking = true;
    }

    public void StartCinematic()
    {
        _uiContainer.SetActive(false);
        StartCoroutine(CinematicBuffer());
    }

    // Buffer created to prevent cutscene being skipped before playing
    IEnumerator CinematicBuffer()
    {
        yield return new WaitForSeconds(2);
        DialogManager.Instance.EnterDialongMode(_openingScript);
        yield return new WaitForSeconds(1);
        _cinematicStarted = true;
    }
}
