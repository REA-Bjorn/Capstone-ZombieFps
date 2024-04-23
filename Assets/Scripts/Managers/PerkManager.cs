using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public static PerkManager Instance;

    // [SerializeField] double health
    // [SerializeField] faster reload
    // [SerializeField] faster shoot
    // [SerializeField] faster sprint
    [SerializeField] private bool secondLife;

    // DoubleHealth property
    // FasterReload property
    // FasterShoot property
    // FasterSprint property
    public bool SecondaryLife => secondLife;

    public Texture FireRateSprite;
    public Texture HealthSprite;
    public Texture ReloadSpeedSprite;
    public Texture SecondLifeSprite;
    public Texture MoveSpeedSprite;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetAllPerks();
    }

    public void ResetAllPerks()
    {
        // reset double health
        // reset faster reload
        // reset faster shoot
        // reset faster sprint
        secondLife = false;

        UIManager.Instance.PerkUIScript.ResetPerks();
    }
}
