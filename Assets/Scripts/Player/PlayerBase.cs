using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamage
{
    public static PlayerBase instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] HealthPool health;

    [SerializeField] SpeedPool speed;

    [SerializeField] PlayerMovement move;

    [SerializeField] BaseCamera cam;

    public SpeedPool Spd => speed;

    public HealthPool Health => health;

    public float ShootDist => WeaponManager.Instance.ShootDist;

    // Start is called before the first frame update
    void Start()
    {
        speed.SetMax();
        health.SetMax();
        health.OnDepleted += HealthDepleted;
    }

    private void HealthDepleted()
    {
        LockPlayer();
        UIManager.Instance.DeathMenu();
    }

    public void LockPlayer()
    {
        move.enabled = false;

        cam.enabled = false;

        WeaponManager.Instance.DisableWeapon();

        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        health.OnDepleted -= HealthDepleted;
    }

    // Update is called once per frame
    void Update()
    {
        move.Movement();
        if (cam.enabled)
        {
            cam.Look();
        }
    }

    public void TakeDamage(float damage)
    {
        health.UseResource(damage);
        UIManager.Instance.HitFlash();
    }

    public void TakeMaxDamage()
    {
        // nothing here, player should never take max damage
    }
}