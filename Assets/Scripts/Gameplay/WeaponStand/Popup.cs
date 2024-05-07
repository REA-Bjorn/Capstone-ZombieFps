using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour, IPopup
{
    [SerializeField] private GameObject displayedObject;

    private void Start()
    {
        HideInteractText();
    }

    public void DisplayInteractText()
    {
        displayedObject.SetActive(true);
    }

    public void HideInteractText()
    {
        displayedObject.SetActive(false);
    }
}
