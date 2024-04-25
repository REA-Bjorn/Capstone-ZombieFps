using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUI : MonoBehaviour
{
    [SerializeField] private SliderScript camSensitivity;
    [SerializeField] private SliderScript camFOV;

    void Start()
    {
        camSensitivity.TurnOn();
        camFOV.TurnOn();

        camSensitivity.SliderUI.value = SettingsManager.Instance.GetSettings().mouseSensitivity;
        camFOV.SliderUI.value = SettingsManager.Instance.GetSettings().fieldOfView;

        camSensitivity.SliderUI.onValueChanged.AddListener(UpdateCamSens);
        camFOV.SliderUI.onValueChanged.AddListener(UpdateCamFOV);
    }

    private void UpdateCamSens(float _newSens)
    {
        SettingsManager.Instance.GetSettings().mouseSensitivity = _newSens;
    }

    private void UpdateCamFOV(float _newFOV)
    {
        SettingsManager.Instance.GetSettings().fieldOfView = _newFOV;
        Camera.main.fieldOfView = _newFOV; // this is also update in the PlayerCamera -> OnEnable()
    }
}
