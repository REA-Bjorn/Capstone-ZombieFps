using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private SettingsSO settingsObject;

    public SettingsSO GetSettings() { return settingsObject; }

    public float MouseSens => settingsObject.mouseSensitivity;
}
