using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDebug : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PerkManager.Instance.UnlockPerk(PerkType.Health);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PerkManager.Instance.UnlockPerk(PerkType.FireRate);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            PerkManager.Instance.UnlockPerk(PerkType.SecondLife);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            PerkManager.Instance.UnlockPerk(PerkType.MoveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PerkManager.Instance.UnlockPerk(PerkType.ReloadSpeed);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            WaveManager.Instance.KillAllAliveEnemies();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PerkManager.Instance.ResetAllPerks();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PointsManager.Instance.AddPoints(500);
        }
    }

}
