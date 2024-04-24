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

    private void Start()
    {
        UpdateMasterVol();
        UpdateMusicVol();
        UpdateSFXVol();
        UpdatePlayerVol();
        UpdateEnemyVol();
        UpdateWeaponVol();
    }

    public void UpdateMasterVol()
    {
        masterGroup.audioMixer.SetFloat("MasterVol", Mathf.Log10(SettingsManager.Instance.GetSettings().masterVol) * 20f);
    }

    public void UpdateMusicVol()
    {
        musicGroup.audioMixer.SetFloat("MusicVol", Mathf.Log10(SettingsManager.Instance.GetSettings().musicVol) * 20f);
    }

    public void UpdateSFXVol()
    {
        sfxGroup.audioMixer.SetFloat("SFXVol", Mathf.Log10(SettingsManager.Instance.GetSettings().sfxVol) * 20f);
    }

    public void UpdatePlayerVol()
    {
        playerGroup.audioMixer.SetFloat("PlayerVol", Mathf.Log10(SettingsManager.Instance.GetSettings().playerVol) * 20f);
    }

    public void UpdateEnemyVol()
    {
        enemyGroup.audioMixer.SetFloat("EnemyVol", Mathf.Log10(SettingsManager.Instance.GetSettings().enemyVol) * 20f);
    }

    public void UpdateWeaponVol()
    {
        weaponGroup.audioMixer.SetFloat("WeaponVol", Mathf.Log10(SettingsManager.Instance.GetSettings().weaponVol) * 20f);
    }
}
