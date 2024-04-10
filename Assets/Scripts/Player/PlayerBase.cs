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

    [SerializeField] PlayerHit hitUI;

    [SerializeField] GameObject playerUI;

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
        //Debug.Log(InputManager.Instance.Action.Attack);
        InputManager.Instance.Action.Attack.started += AttackStart;
        health.OnDepleted += HealthDepleted;
    }

    private void HealthDepleted()
    {
        LockPlayer();
        UIManager.Instance.DeathMenu();
        hitUI.PlayerDiedVignette();
    }

    public void LockPlayer()
    {
        playerUI.SetActive(false);

        move.enabled = false;

        cam.enabled = false;

        InputManager.Instance.Action.Attack.started -= AttackStart;

        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        InputManager.Instance.Action.Attack.started -= AttackStart;
        health.OnDepleted -= HealthDepleted;
    }

    private void AttackStart(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        WeaponManager.Instance.Shoot();
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
        hitUI.Active();
    }
}