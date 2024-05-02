using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

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
}
