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

    [SerializeField] WeaponBase weapon;

    [SerializeField] PlayerMovement move;

    [SerializeField] BaseCamera cam;

    public SpeedPool Spd => speed;

    public float ShootDist => weapon.ShootDist;

    // Start is called before the first frame update
    void Start()
    {
        speed.SetMax();
        health.SetMax();
        Debug.Log(InputManager.Instance.Action.Attack);
        InputManager.Instance.Action.Attack.started += Attack_started;
        health.OnDepleted += Health_OnDepleted;
    }

    private void Health_OnDepleted()
    {
        LockPlayer();
        UIManager.Instance.DeathMenu();
    }

    public void LockPlayer()
    {
        playerUI.SetActive(false);

        move.enabled = false;

        cam.enabled = false;

        InputManager.Instance.Action.Attack.started -= Attack_started;

        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        InputManager.Instance.Action.Attack.started -= Attack_started;
        health.OnDepleted -= Health_OnDepleted;
    }

    private void Attack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        weapon.Shoot();
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
        Debug.Log("Player is Damaged");

        health.UseResource(damage);
        hitUI.Active();
    }
}