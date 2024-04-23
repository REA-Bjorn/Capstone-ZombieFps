using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject volumeObject;
    [SerializeField] private GameObject cameraOptions;
    [Seperator]
    [SerializeField] private Button volumeBtn;
    [SerializeField] private Button cameraBtn;

    private void Start()
    {
        volumeBtn.onClick.AddListener(TurnOnVolumeOptions);
        cameraBtn.onClick.AddListener(TurnOnCameraOptions);
    }

    public void TurnOnVolumeOptions()
    {
        volumeObject.SetActive(true);
        cameraOptions.SetActive(false);
    }

    public void TurnOnCameraOptions()
    {
        volumeObject.SetActive(false);
        cameraOptions.SetActive(true);
    }
    
}
