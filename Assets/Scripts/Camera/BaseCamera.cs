using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


public class BaseCamera : MonoBehaviour
{
    [SerializeField] private float vertLookValue;
    [SerializeField] private float camHoriSpeed;
    [SerializeField] private float camVertSpeed;

    float xRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        AimCamera();
    }

    private void AimCamera()
    {
        Vector2 input = InputManager.Instance.CameraReadVal();
        xRot = -input.y * camVertSpeed * Time.deltaTime;
        //xRot = Mathf.Clamp(xRot, -vertLookValue, vertLookValue);

        transform.Rotate(Vector3.right * xRot);

        if (Mathf.Abs(input.x) > 0)
        {
            transform.parent.Rotate(Vector3.up * input.x * Time.deltaTime * camHoriSpeed);
        }
    }
}

