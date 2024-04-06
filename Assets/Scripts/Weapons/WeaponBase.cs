using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTimer))]
public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected AttackPool attack;

    [SerializeField] protected CustomTimer timer;

    [SerializeField] protected float range;

    [SerializeField] protected float cooldown;

    [SerializeField] protected ParticleSystem muzzleFlash;

    protected bool CanUse;
    public float ShootDist => range;

    // Start is called before the first frame update
    void Start()
    {
        attack.SetMax();
        CanUse = true;
    }

    private void OnEnable()
    {
        timer.OnEnd += TimerEnd;
        timer.OnStart += TimerStart;
        timer.OnRestart += TimerStart;
    }

    private void OnDisable()
    {
        timer.OnEnd -= TimerEnd;
        timer.OnStart -= TimerStart;
        timer.OnRestart -= TimerStart;
    }

    private void TimerStart()
    {
        CanUse = false;
    }

    private void TimerEnd()
    {
        CanUse = true;
        timer.enabled = false;
    }

    public virtual void Shoot()
    {
        //nothing should be here
        Debug.Log("Base Funtion Called Find Out Why -> Usually due to base.Shoot() being used!");
    }
}
