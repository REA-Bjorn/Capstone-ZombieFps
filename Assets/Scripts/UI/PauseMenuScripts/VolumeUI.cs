using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class VolumeUI : MonoBehaviour
{
    [SerializeField] private SliderScript master;
    [SerializeField] private SliderScript music;
    [SerializeField] private SliderScript sfx;
    [SerializeField] private SliderScript player;
    [SerializeField] private SliderScript enemy;
    [SerializeField] private SliderScript weapon;

    private void Start()
    {
        master.TurnOn();
        music.TurnOn();
        sfx.TurnOn();
        player.TurnOn();
        enemy.TurnOn();
        weapon.TurnOn();

        UpdateThemAll();

        Subscribe();
    }

    private void UpdateThemAll()
    {
        // Update Preexisting slider values
        master.SliderUI.value = PlayerPrefs.GetFloat("MasterVol");
        music.SliderUI.value = PlayerPrefs.GetFloat("MusicVol");
        sfx.SliderUI.value = PlayerPrefs.GetFloat("SFXVol");
        player.SliderUI.value = PlayerPrefs.GetFloat("PlayerVol");
        enemy.SliderUI.value = PlayerPrefs.GetFloat("EnemyVol");
        weapon.SliderUI.value = PlayerPrefs.GetFloat("WeaponVol");
    }

    private void Subscribe()
    {
        master.SliderUI.onValueChanged.AddListener(MasterVolChanged);
        music.SliderUI.onValueChanged.AddListener(MusicVolChanged);
        sfx.SliderUI.onValueChanged.AddListener(SFXVolChanged);
        player.SliderUI.onValueChanged.AddListener(PlayerVolChanged);
        enemy.SliderUI.onValueChanged.AddListener(EnemyVolChanged);
        weapon.SliderUI.onValueChanged.AddListener(WeaponVolChanged);
    }

    private void MasterVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().masterVol = Mathf.Clamp(value, master.Min, master.Max);
        AudioManager.Instance.UpdateMasterVol();
    }

    private void MusicVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().musicVol = Mathf.Clamp(value, music.Min, music.Max);
        AudioManager.Instance.UpdateMusicVol();
    }

    private void SFXVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().sfxVol = Mathf.Clamp(value, sfx.Min, sfx.Max);
        AudioManager.Instance.UpdateSFXVol();
    }

    private void PlayerVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().playerVol = Mathf.Clamp(value, player.Min, player.Max);
        AudioManager.Instance.UpdatePlayerVol();
    }

    private void EnemyVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().enemyVol = Mathf.Clamp(value, enemy.Min, enemy.Max);
        AudioManager.Instance.UpdateEnemyVol();
    }

    private void WeaponVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().weaponVol = Mathf.Clamp(value, weapon.Min, weapon.Max);
        AudioManager.Instance.UpdateWeaponVol();
    }
}
