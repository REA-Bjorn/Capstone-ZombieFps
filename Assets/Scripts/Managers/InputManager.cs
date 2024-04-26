using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    PlayerInputs inputs;

    public PlayerInputs.GeneralActions Action => inputs.General;

    public bool SprintON => Input.GetKey(KeyCode.LeftShift);
    public bool SprintOff => Input.GetKeyUp(KeyCode.LeftShift);

    public Vector2 MoveVect => inputs.General.Movement.ReadValue<Vector2>();

    public Vector2 Look => inputs.General.Looking.ReadValue<Vector2>();

    public Vector2 ScrollVect => inputs.General.ScrollWeapon.ReadValue<Vector2>();

    public bool PlayerMoved => inputs.General.Movement.ReadValue<Vector2>().x > 0 || inputs.General.Movement.ReadValue<Vector2>().y > 0;

    private void Awake()
    {
        Instance = this;
        inputs = new PlayerInputs();
        inputs.Enable();
    }


    // Start is called before the first frame update
    void Start()
    {
        // subscribe methods
        inputs.General.ScrollWeapon.performed += WeaponManager.Instance.ToggleWeapon;
        inputs.General.Attack.performed += WeaponManager.Instance.Shoot;
        inputs.PauseActions.Pause.started += UIManager.Instance.PauseMenu;
    }

    private void OnDestroy()
    {
        // unsubscribe methods
        inputs.General.ScrollWeapon.performed -= WeaponManager.Instance.ToggleWeapon;
        inputs.General.Attack.performed -= WeaponManager.Instance.Shoot;
        inputs.PauseActions.Pause.started -= UIManager.Instance.PauseMenu;
    }

    public Vector2 CameraReadVal()
    {
        return inputs.General.Looking.ReadValue<Vector2>();
    }

    public void UnPauseActions()
    {
        inputs.General.Enable();
    }

    public void PauseActions()
    {
        inputs.General.Disable();
    }
}
