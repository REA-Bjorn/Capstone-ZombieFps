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

        SettingsManager.Instance.GetSettings().mouseSensitivity = PlayerPrefs.GetFloat("MouseSens");
        SettingsManager.Instance.GetSettings().fieldOfView = PlayerPrefs.GetFloat("FOV");
        SettingsManager.Instance.GetSettings().invertY = PlayerPrefs.GetInt("InvertY") == 1 ? true : false;

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
        PlayerPrefs.SetFloat("MouseSens", _newSens);
        PlayerPrefs.Save();
    }

    private void UpdateCamInvertY(bool _status)
    {
        SettingsManager.Instance.GetSettings().invertY = _status;
        PlayerPrefs.SetFloat("InvertY", _status ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateCamFOV(float _newFOV)
    {
        SettingsManager.Instance.GetSettings().fieldOfView = _newFOV;
        PlayerPrefs.SetFloat("FOV", _newFOV);
        PlayerPrefs.Save();
        Camera.main.fieldOfView = _newFOV; // this is also update in the PlayerCamera -> OnEnable()
    }
}
