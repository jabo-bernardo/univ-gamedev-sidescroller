using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.5f;

    public void StartShake() {
        StartCoroutine(CameraShakeHandler());
    }

    private IEnumerator CameraShakeHandler() {
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-shakeAmount, shakeAmount);
            float yOffset = Random.Range(-shakeAmount, shakeAmount);
            transform.position = initialPosition + new Vector3(xOffset, yOffset, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = initialPosition;
    }
}
