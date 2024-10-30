using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float _shakeTime;
    [SerializeField] float _shakeRadius;

    public bool isShaking;
    Vector3 _shakeOffset;

    void Update()
    {
        // Turn off shaking when game is paused
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

    // Shake the camera for an amount of time
    IEnumerator Shake()
    {
        isShaking = true;
        yield return new WaitForSeconds(_shakeTime);
        isShaking = false;
    }
}
