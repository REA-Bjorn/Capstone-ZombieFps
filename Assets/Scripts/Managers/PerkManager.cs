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

    private void Awake()
    {
        Instance = this;
    }

    public void ResetAllPerks()
    {
        // reset double health
        // reset faster reload
        // reset faster shoot
        // reset faster sprint
        secondLife = false;
    }
}
