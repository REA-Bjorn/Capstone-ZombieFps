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
        SettingsManager.Instance.GetSettings().radialStamina = PlayerPrefs.GetInt("RadialStamina") == 1 ? true : false;
        SettingsManager.Instance.GetSettings().fullscreen = PlayerPrefs.GetInt("FullScreen") == 1 ? true : false;
        SettingsManager.Instance.GetSettings().postProcessing = PlayerPrefs.GetInt("PostProcessing") == 1 ? true : false;

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
        PlayerPrefs.SetInt("RadialStamina", _state ? 1 : 0);
        PlayerPrefs.Save();
        UIManager.Instance.PlayerUIScript.ToggleRadialStamina(_state);
    }

    private void UpdateFullScreen(bool _state)
    {
        SettingsManager.Instance.GetSettings().fullscreen = _state;
        PlayerPrefs.SetInt("FullScreen", _state ? 1 : 0);
        PlayerPrefs.Save();
        Screen.fullScreen = _state;
    }

    private void UpdatePostProcess(bool _state)
    {
        SettingsManager.Instance.GetSettings().postProcessing = _state;
        PlayerPrefs.SetInt("PostProcessing", _state ? 1 : 0);
        PlayerPrefs.Save();
        PostProcessManager.Instance?.TogglePostProcess(_state);
    }
}
