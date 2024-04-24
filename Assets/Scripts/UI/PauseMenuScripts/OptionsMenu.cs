using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject volumeObject;
    [SerializeField] private GameObject cameraOptions;
    [SerializeField] private GameObject graphicsOptions;
    [Seperator]
    [SerializeField] private Button volumeBtn;
    [SerializeField] private Button cameraBtn;
    [SerializeField] private Button graphicsBtn;

    private void Start()
    {
        volumeBtn.onClick.AddListener(TurnOnVolumeOptions);
        cameraBtn.onClick.AddListener(TurnOnCameraOptions);
        graphicsBtn.onClick.AddListener(TurnOnGraphicsOptions);
    }

    public void TurnOnVolumeOptions()
    {
        cameraOptions.SetActive(false);
        graphicsOptions.SetActive(false);

        volumeObject.SetActive(true);
    }

    public void TurnOnCameraOptions()
    {
        volumeObject.SetActive(false);
        graphicsOptions.SetActive(false);
        
        cameraOptions.SetActive(true);
    }

    public void TurnOnGraphicsOptions()
    {
        cameraOptions.SetActive(false);
        volumeObject.SetActive(false);

        graphicsOptions.SetActive(true);
    }
}
