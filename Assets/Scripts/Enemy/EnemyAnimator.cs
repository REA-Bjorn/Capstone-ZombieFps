using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void PlayRunAnimation()
    {
        anim.SetTrigger("FinishedSpawn");
    }

    public void PlayHitAnimation()
    {
        anim.SetBool("GotHit", true);
    }

    public void StopHitAnimation()
    {
        anim.SetBool("GotHit", false);
    }

    public void PlayAttackAnimation()
    {
        anim.SetBool("Attacking", true);
    }

    public void StopAttackAnimation()
    {
        anim.SetBool("Attacking", false);
    }

    public void PlayDeathAnimation()
    {
        anim.SetTrigger("Killed");
    }

    public void UpdateRootMotion(bool _state)
    {
        anim.applyRootMotion = _state;
    }
}
