using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraUI : MonoBehaviour
{
    [SerializeField] private SliderScript camSensitivity;
    [SerializeField] private SliderScript camFOV;
    [SerializeField] private Toggle invertLookAxis;

    void Start()
    {
        camSensitivity.TurnOn();
        camFOV.TurnOn();

        camSensitivity.SliderUI.value = SettingsManager.Instance.GetSettings().mouseSensitivity;
        camFOV.SliderUI.value = SettingsManager.Instance.GetSettings().fieldOfView;
        invertLookAxis.isOn = SettingsManager.Instance.GetSettings().invertY;

        camSensitivity.SliderUI.onValueChanged.AddListener(UpdateCamSens);
        camFOV.SliderUI.onValueChanged.AddListener(UpdateCamFOV);
        invertLookAxis.onValueChanged.AddListener(UpdateCamInvertY);
    }

    private void UpdateCamSens(float _newSens)
    {
        SettingsManager.Instance.GetSettings().mouseSensitivity = _newSens;
    }

    private void UpdateCamInvertY(bool _status)
    {
        SettingsManager.Instance.GetSettings().invertY = _status;
    }

    private void UpdateCamFOV(float _newFOV)
    {
        SettingsManager.Instance.GetSettings().fieldOfView = _newFOV;
        Camera.main.fieldOfView = _newFOV; // this is also update in the PlayerCamera -> OnEnable()
    }
}
