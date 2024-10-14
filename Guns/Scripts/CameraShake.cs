using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0f;
    public float shakeIntensity = 0.1f;
    public float shakeDecay = 1.0f;

    private Vector3 originalPosition;
    private float shakeTimer = 0f;

    private void Start()
    {
        originalPosition = transform.position;
    }
    private void Update()
    {
        if (shakeTimer > 0)
        {
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;
            shakeTimer -= Time.deltaTime * shakeDecay;
        }
        else
        {
            shakeTimer = 0f;
            transform.position = originalPosition;
        }
    }

    public void Shake(float duration, float intensity, float decay)
    {
        shakeDuration = duration;
        shakeIntensity = intensity;
        shakeDecay = decay;
        shakeTimer = duration;
    }
}
