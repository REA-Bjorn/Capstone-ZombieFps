using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


public class BaseCamera : MonoBehaviour
{
    [SerializeField] public float mouseSenitivity;

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = InputManager.Instance.Action.Looking.ReadValue<Vector2>().x * mouseSenitivity * Time.deltaTime;
        float mouseY = InputManager.Instance.Action.Looking.ReadValue<Vector2>().y * mouseSenitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

