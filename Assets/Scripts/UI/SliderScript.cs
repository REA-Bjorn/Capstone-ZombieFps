using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private string sliderName;
    [SerializeField] private float minVal;
    [SerializeField] private float maxVal;
    [Seperator]
    [SerializeField] private Slider sliderUI;
    [SerializeField] private TextMeshProUGUI displayText;

    public Slider SliderUI => sliderUI;

    public void TurnOn()
    {
        sliderUI.minValue = minVal;
        sliderUI.maxValue = maxVal;
        displayText.text = sliderName;
    }
}
