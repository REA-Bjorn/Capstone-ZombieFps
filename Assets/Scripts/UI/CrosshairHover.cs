using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class CrosshairHover : MonoBehaviour
{
    [SerializeField] private Image crossHair;

    public void ChangeCrosshairColor(Color _color)
    {
        crossHair.color = _color;
    }
}
