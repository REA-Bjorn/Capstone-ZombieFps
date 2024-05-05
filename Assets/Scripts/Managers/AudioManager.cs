using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private AudioMixerGroup masterGroup;
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;
    [SerializeField] private AudioMixerGroup playerGroup;
    [SerializeField] private AudioMixerGroup enemyGroup;
    [SerializeField] private AudioMixerGroup weaponGroup;
    [SerializeField] private AudioSource musicSource;

    private void Start()
    {
        // Load Saved Data

        SettingsManager.Instance.GetSettings().masterVol = PlayerPrefs.GetFloat("MasterVol");
        SettingsManager.Instance.GetSettings().musicVol = PlayerPrefs.GetFloat("MusicVol");
        SettingsManager.Instance.GetSettings().sfxVol = PlayerPrefs.GetFloat("SFXVol");
        SettingsManager.Instance.GetSettings().playerVol = PlayerPrefs.GetFloat("PlayerVol");
        SettingsManager.Instance.GetSettings().enemyVol = PlayerPrefs.GetFloat("EnemyVol");
        SettingsManager.Instance.GetSettings().weaponVol = PlayerPrefs.GetFloat("WeaponVol");

        // Set values of saved data

        masterGroup.audioMixer.SetFloat("MasterVol", Mathf.Log10(SettingsManager.Instance.GetSettings().masterVol) * 20f);
        musicGroup.audioMixer.SetFloat("MusicVol", Mathf.Log10(SettingsManager.Instance.GetSettings().musicVol) * 20f);
        sfxGroup.audioMixer.SetFloat("SFXVol", Mathf.Log10(SettingsManager.Instance.GetSettings().sfxVol) * 20f);
        playerGroup.audioMixer.SetFloat("PlayerVol", Mathf.Log10(SettingsManager.Instance.GetSettings().playerVol) * 20f);
        enemyGroup.audioMixer.SetFloat("EnemyVol", Mathf.Log10(SettingsManager.Instance.GetSettings().enemyVol) * 20f);
        weaponGroup.audioMixer.SetFloat("WeaponVol", Mathf.Log10(SettingsManager.Instance.GetSettings().weaponVol) * 20f);

        musicSource.Play();
    }

    public void UpdateMasterVol()
    {
        masterGroup.audioMixer.SetFloat("MasterVol", Mathf.Log10(SettingsManager.Instance.GetSettings().masterVol) * 20f);
        PlayerPrefs.SetFloat("MasterVol", SettingsManager.Instance.GetSettings().masterVol);
        PlayerPrefs.Save();
    }

    public void UpdateMusicVol()
    {
        musicGroup.audioMixer.SetFloat("MusicVol", Mathf.Log10(SettingsManager.Instance.GetSettings().musicVol) * 20f);
        PlayerPrefs.SetFloat("MusicVol", SettingsManager.Instance.GetSettings().musicVol);
        PlayerPrefs.Save();
    }

    public void UpdateSFXVol()
    {
        sfxGroup.audioMixer.SetFloat("SFXVol", Mathf.Log10(SettingsManager.Instance.GetSettings().sfxVol) * 20f);
        PlayerPrefs.SetFloat("SFXVol", SettingsManager.Instance.GetSettings().sfxVol);
        PlayerPrefs.Save();
    }

    public void UpdatePlayerVol()
    {
        playerGroup.audioMixer.SetFloat("PlayerVol", Mathf.Log10(SettingsManager.Instance.GetSettings().playerVol) * 20f);
        PlayerPrefs.SetFloat("PlayerVol", SettingsManager.Instance.GetSettings().playerVol);
        PlayerPrefs.Save();
    }

    public void UpdateEnemyVol()
    {
        enemyGroup.audioMixer.SetFloat("EnemyVol", Mathf.Log10(SettingsManager.Instance.GetSettings().enemyVol) * 20f);
        PlayerPrefs.SetFloat("EnemyVol", SettingsManager.Instance.GetSettings().enemyVol);
        PlayerPrefs.Save();
    }

    public void UpdateWeaponVol()
    {
        weaponGroup.audioMixer.SetFloat("WeaponVol", Mathf.Log10(SettingsManager.Instance.GetSettings().weaponVol) * 20f);
        PlayerPrefs.SetFloat("WeaponVol", SettingsManager.Instance.GetSettings().weaponVol);
        PlayerPrefs.Save();
    }
}
