using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    PlayerInputs inputs;

    public PlayerInputs.GeneralActions Action => inputs.General;

    public Vector2 MoveVect => inputs.General.Movement.ReadValue<Vector2>();

    private void Awake()
    {
        Instance = this;
        inputs = new PlayerInputs();
        inputs.Enable();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 CameraReadVal()
    {
        return inputs.General.Looking.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
