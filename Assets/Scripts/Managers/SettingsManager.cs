using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [SerializeField] private SettingsSO settingsObject;

    public SettingsSO GetSettings() { return settingsObject; }
    public float MouseSens => settingsObject.mouseSensitivity;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartGraphics();
        StartAudio();
        StartCamera();
    }

    private void StartCamera()
    {
        settingsObject.mouseSensitivity = PlayerPrefs.GetFloat("MouseSens");
        settingsObject.fieldOfView = PlayerPrefs.GetFloat("FOV");
        settingsObject.invertY = PlayerPrefs.GetInt("InvertY") == 1 ? true : false;
        Camera.main.fieldOfView = settingsObject.fieldOfView;
    }

    private void StartAudio()
    {
        // Load Saved Data
        settingsObject.masterVol = PlayerPrefs.GetFloat("MasterVol");
        settingsObject.musicVol = PlayerPrefs.GetFloat("MusicVol");
        settingsObject.sfxVol = PlayerPrefs.GetFloat("SFXVol");
        settingsObject.playerVol = PlayerPrefs.GetFloat("PlayerVol");
        settingsObject.enemyVol = PlayerPrefs.GetFloat("EnemyVol");
        settingsObject.weaponVol = PlayerPrefs.GetFloat("WeaponVol");
    }

    private void StartGraphics()
    {
        bool radStam = PlayerPrefs.GetInt("RadialStamina") == 1 ? true : false;
        bool full = PlayerPrefs.GetInt("FullScreen") == 1 ? true : false;
        bool postfx = PlayerPrefs.GetInt("PostProcessing") == 1 ? true : false;

        settingsObject.radialStamina = radStam;
        settingsObject.fullscreen = full;
        settingsObject.postProcessing = postfx;

        PostProcessManager.Instance?.TogglePostProcess(radStam);
        Screen.fullScreen = full;
        UIManager.Instance.PlayerUIScript.ToggleRadialStamina(postfx);
    }
}
