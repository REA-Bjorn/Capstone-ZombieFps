using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

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
    }

    private void OnDestroy()
    {
        
    }
}
