using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTimer), typeof(Animator))]
public class KnifeWeapon : MonoBehaviour
{
    [SerializeField] private CustomTimer knifeTimer;
    [SerializeField] private AudioClip knifeSwooshSFX;
    [SerializeField] private Animator knifeAnimator;
    [SerializeField] private GameObject gunHolder;

    public void CallUseKnife()
    {
        if (!knifeTimer.RunTimer)
        {
            knifeTimer.StartTimer();
            UseKnife();
        }
    }

    private void UseKnife()
    {
        // SFX/VFX
        PlayerBase.instance.ExtraSFX(knifeSwooshSFX);
        knifeAnimator.SetTrigger("Slash");
        TurnGunsOff();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out hit, 3))
        {
            IDamage damage = hit.collider.GetComponent<IDamage>();

            if (hit.collider.CompareTag("Player"))
                return;

            if (damage != null)
            {
                UIManager.Instance.PlayerUIScript.FlashHitMarker();

                if (WeaponManager.Instance.InstaKill)
                {
                    damage.TakeMaxDamage();
                }
                else
                {
                    damage.TakeDamage((WaveManager.Instance.CurrWaveNumInt / 3f) + 0.5f);
                }
            }
        }
    }

    public void TurnGunsOn()
    {
        gunHolder?.SetActive(true);
        // Fix for reloading after knifing (ondisable was called)
        WeaponManager.Instance.CurrentWeapon.WeaponOn();
    }

    private void TurnGunsOff()
    {
        gunHolder?.SetActive(false);
    }
}
