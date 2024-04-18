using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Basic Camera Information
    [SerializeField] public float mouseSenitivity;
    public Transform playerBody;
    private float xRotation = 0f;

    // Hold Variables
    private float shakeAmount = 2f;
    private float decreaseFactor = 1.0f;
    private float shakeDuration = 0f;
    private Vector3 originalPos;
    private bool shakeCam = false;

    private void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    public void Look()
    {
        float mouseX = InputManager.Instance.Look.x * mouseSenitivity * Time.deltaTime;
        float mouseY = InputManager.Instance.Look.y * mouseSenitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (shakeCam)
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos + Random.insideUnitSphere * shakeAmount, Time.deltaTime * 3);
                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0; // reset because cam shake should pass in the amount
                transform.localPosition = originalPos;
                shakeCam = false;
            }
        }
    }

    public void TurnOnCamShake(float _shakeAmt, float _shakeDuration)
    {
        shakeAmount = _shakeAmt;
        shakeDuration = _shakeDuration;
        shakeCam = true;
    }
}
