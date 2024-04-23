using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }



    private void OnDestroy()
    {
        
    }
}
