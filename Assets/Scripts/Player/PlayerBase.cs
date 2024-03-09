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

    [SerializeField] CharacterController charController;

    [SerializeField] BaseCamera cam;

    [SerializeField] HealthPool health;

    [SerializeField] SpeedPool speed;

    [SerializeField] WeaponBase weapon;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        speed.SetMax();
        health.SetMax();
        Debug.Log(InputManager.Instance.Action.Attack);
        InputManager.Instance.Action.Attack.started += Attack_started;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        InputManager.Instance.Action.Attack.started -= Attack_started;
    }

    private void Attack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        weapon.Shoot();
    }

    public void Movement()
    {
        movement = (transform.right * InputManager.Instance.MoveVect.x) + (transform.forward * InputManager.Instance.MoveVect.y);

        charController.Move(movement * Time.deltaTime * speed.CurrentValue);

        //Old Code
        //Vector2 move = InputManager.Instance.MoveVect * speed.CurrentValue;
        //rb.velocity = transform.TransformDirection(new Vector3(move.x, rb.velocity.y, move.y));
    }
    

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
