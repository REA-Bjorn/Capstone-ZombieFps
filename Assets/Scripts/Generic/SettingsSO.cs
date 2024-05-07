using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings Object", menuName = "Settings")]
public class SettingsSO : ScriptableObject
{
    // Audios
    public float masterVol;
    public float musicVol;
    public float playerVol;
    public float sfxVol;
    public float enemyVol;
    public float weaponVol;

    [Seperator]
    // Other
    public float fieldOfView;
    public float mouseSensitivity;
    public bool invertY;

    [Seperator]
    public bool fullscreen;
    public bool postProcessing;
    public bool radialStamina;
}
