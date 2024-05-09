using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupCooldownUI : MonoBehaviour
{
    [SerializeField] private Image DoublePts;
    [SerializeField] private Image InstaKill;

    [SerializeField] private CustomTimer doubleptsTimer;
    [SerializeField] private CustomTimer instakillTimer;

    // Start is called before the first frame update
    void Start()
    {
        doubleptsTimer.OnTick += () =>
        {
            DoublePts.fillAmount = doubleptsTimer.Percentage;
        };
        instakillTimer.OnTick += () =>
        {
            InstaKill.fillAmount = instakillTimer.Percentage;
        };

        DoublePts.fillAmount = 0;
        InstaKill.fillAmount = 0;
    }

    public void CallDoublePts()
    {
        doubleptsTimer.StartTimer(_override:true);
    }

    public void CallInstaKill()
    {
        instakillTimer.StartTimer(_override: true);
    }
}
