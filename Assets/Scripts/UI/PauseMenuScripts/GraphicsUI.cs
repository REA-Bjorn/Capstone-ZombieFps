using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GraphicsUI : MonoBehaviour
{
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private Toggle postProcessing;

    void Start()
    {
        fullScreen.isOn = SettingsManager.Instance.GetSettings().fullscreen;
        postProcessing.isOn = SettingsManager.Instance.GetSettings().postProcessing;

        fullScreen.onValueChanged.AddListener(UpdateFullScreen);
        postProcessing.onValueChanged.AddListener(UpdatePostProcess);
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
