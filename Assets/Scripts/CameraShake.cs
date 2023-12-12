using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraShake : MonoBehaviour
{
[SerializeField] float shakeDuration = 1f;
[SerializeField] float shakeMagnitude = 0.5f;
[SerializeField] float timePerShakeUp = 0.2f;

Vector3 initialPosition;

void Start()
{
    initialPosition = transform.position;
}

public void PlayCameraShake()
{
    StartCoroutine(Shake());
}

IEnumerator Shake()
{
    float shakeDurationTimer = shakeDuration;
    while (shakeDurationTimer >= 0)
    {
        transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
        yield return new WaitForSeconds(timePerShakeUp);
        shakeDurationTimer -= timePerShakeUp;
    }
    transform.position = initialPosition;

}
}
