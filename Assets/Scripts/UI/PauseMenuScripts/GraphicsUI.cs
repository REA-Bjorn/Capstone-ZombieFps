using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GraphicsUI : MonoBehaviour
{
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Toggle postProcessing;
    [SerializeField] private Toggle radialStaminaIndicator;

    void Start()
    {
        fullScreen.isOn = SettingsManager.Instance.GetSettings().fullscreen;
        postProcessing.isOn = SettingsManager.Instance.GetSettings().postProcessing;
        radialStaminaIndicator.isOn = SettingsManager.Instance.GetSettings().radialStamina;

        fullScreen.onValueChanged.AddListener(UpdateFullScreen);
        postProcessing.onValueChanged.AddListener(UpdatePostProcess);
        radialStaminaIndicator.onValueChanged.AddListener(UpdateRadialStamina);
    }

    private void UpdateRadialStamina(bool _state)
    {
        SettingsManager.Instance.GetSettings().radialStamina = _state;
        UIManager.Instance.PlayerUIScript.ToggleRadialStamina(_state);
    }

    private void UpdateFullScreen(bool _state)
    {
        SettingsManager.Instance.GetSettings().fullscreen = _state;
        Screen.fullScreen = _state;
    }

    private void UpdatePostProcess(bool _state)
    {
        SettingsManager.Instance.GetSettings().postProcessing = _state;
        //Camera.main.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = _state;
    }
}
