using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float _shakeTime;
    [SerializeField] float _shakeRadius;

    bool isShaking;
    Vector3 _shakeOffset;

    void Update()
    {
        if (Time.timeScale == 0)
            isShaking = false;

        // Generate a shaking offset if the camera is shaking
        if (isShaking)
            _shakeOffset = Random.insideUnitCircle * _shakeRadius;
        else
            _shakeOffset = Vector3.zero;

        transform.position += _shakeOffset;
    }

    public void StartShaking()
    {
        if (!isShaking)
            StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        isShaking = true;
        yield return new WaitForSeconds(_shakeTime);
        isShaking = false;
    }
}
