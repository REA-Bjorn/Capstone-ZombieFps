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

    private bool sprinting;

    public bool SprintON => Input.GetKey(KeyCode.LeftShift) || sprinting;
    public bool SprintOff => Input.GetKeyUp(KeyCode.LeftShift) || !sprinting;
    public bool IsInteracting => inputs.General.Interact.WasPerformedThisFrame();

    public Vector2 MoveVect => inputs.General.Movement.ReadValue<Vector2>();

    public Vector2 Look => inputs.General.Looking.ReadValue<Vector2>();

    public Vector2 ScrollVect => inputs.General.ScrollWeapon.ReadValue<Vector2>();

    public bool PlayerMoved
    {
        get
        {
            // Avoids garbage memory / using a function twice
            Vector2 test = inputs.General.Movement.ReadValue<Vector2>();
            return Mathf.Abs(test.x) > 0 || Mathf.Abs(test.y) > 0;
        }
    }

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
        inputs.General.ToggleWeapon.performed += WeaponManager.Instance.ToggleWeapon;
        inputs.General.Attack.performed += WeaponManager.Instance.Shoot;
        inputs.General.UseKnife.started += WeaponManager.Instance.UseKnifePressed;
        inputs.PauseActions.Pause.started += UIManager.Instance.PauseMenu;
    }

    private void OnDestroy()
    {
        // unsubscribe methods
        inputs.General.ScrollWeapon.performed -= WeaponManager.Instance.ToggleWeapon;
        inputs.General.ToggleWeapon.performed -= WeaponManager.Instance.ToggleWeapon;
        inputs.General.Attack.performed -= WeaponManager.Instance.Shoot;
        inputs.General.UseKnife.started -= WeaponManager.Instance.UseKnifePressed;
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

    private void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            // Controller Specific fix
            if (Action.Sprint.WasPressedThisFrame() && !sprinting)
            {
                sprinting = true;
            }
            else if (Action.Sprint.WasReleasedThisFrame() && sprinting)
            {
                sprinting = false;
            }
        }
    }
}
