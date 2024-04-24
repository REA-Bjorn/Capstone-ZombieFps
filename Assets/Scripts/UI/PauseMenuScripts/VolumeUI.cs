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
        master.SliderUI.value = SettingsManager.Instance.GetSettings().masterVol;
        music.SliderUI.value = SettingsManager.Instance.GetSettings().musicVol;
        sfx.SliderUI.value = SettingsManager.Instance.GetSettings().sfxVol;
        player.SliderUI.value = SettingsManager.Instance.GetSettings().playerVol;
        enemy.SliderUI.value = SettingsManager.Instance.GetSettings().enemyVol;
        weapon.SliderUI.value = SettingsManager.Instance.GetSettings().weaponVol;
       
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
        SettingsManager.Instance.GetSettings().masterVol = value;
        AudioManager.Instance.UpdateMasterVol();
    }

    private void MusicVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().musicVol = value;
        AudioManager.Instance.UpdateMusicVol();
    }

    private void SFXVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().sfxVol = value;
        AudioManager.Instance.UpdateSFXVol();
    }

    private void PlayerVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().playerVol = value;
        AudioManager.Instance.UpdatePlayerVol();
    }

    private void EnemyVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().enemyVol = value;
        AudioManager.Instance.UpdateEnemyVol();
    }

    private void WeaponVolChanged(float value)
    {
        SettingsManager.Instance.GetSettings().weaponVol = value;
        AudioManager.Instance.UpdateWeaponVol();
    }
}
