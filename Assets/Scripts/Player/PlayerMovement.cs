using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movement;

    [SerializeField] CharacterController charController;

    private void OnDisable()
    {
        charController.enabled = false;
    }

    private void OnEnable()
    {
        charController.enabled = true;  
    }

    void Start()
    {

    }

    public void Movement()
    {
        if (PlayerBase.instance.Health.IsValid)
        {
            movement = (transform.right * InputManager.Instance.MoveVect.x) + (transform.forward * InputManager.Instance.MoveVect.y);
            charController.Move(movement * Time.deltaTime * PlayerBase.instance.Spd.CurrentValue);
            charController.Move(new Vector3(0, -9.81f * Time.deltaTime, 0));
        }
    }

}
