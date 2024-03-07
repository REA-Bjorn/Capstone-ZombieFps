using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] BaseCamera cam;

    [SerializeField] HealthPool health;

    [SerializeField] SpeedPool speed;

    [SerializeField] WeaponBase weapon;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        speed.SetMax();
        rb = GetComponent<Rigidbody>();
        InputManager.Instance.Action.Attack.started += Attack_started;
        health.SetMax();
    }

    private void Attack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        weapon.Shoot();
    }

    public void Movement()
    {
        Vector2 move = InputManager.Instance.MoveVect * speed.CurrentValue;
        rb.velocity = transform.TransformDirection(new Vector3(move.x, rb.velocity.y, move.y));
    }
    

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
