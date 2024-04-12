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
    // SFX
    // Player
    // Enemy

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SettingsManager.Instance.GetSettings().masterVol -= .01f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SettingsManager.Instance.GetSettings().masterVol += .01f;
        }

        UpdateMasterVol();
    }

    public void UpdateMasterVol()
    {
        masterGroup.audioMixer.SetFloat("MasterVol", Mathf.Log10(SettingsManager.Instance.GetSettings().masterVol) * 20f);
    }
}
