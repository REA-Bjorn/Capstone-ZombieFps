using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class CrosshairHover : MonoBehaviour
{
    [SerializeField] PlayerBase player;

    RaycastHit hit;

    Image crosshair;

    void Start()
    {
        crosshair = GetComponentInChildren<Image>();
        crosshair.GetComponent<Image>().color = Color.white;
    }

    void Update()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, player.ShootDist) && hit.collider.CompareTag("Enemy"))
        {
            crosshair.GetComponent<Image>().color = Color.red;
        }
        else
        {
            crosshair.GetComponent<Image>().color = Color.white;
        }
    }
}
