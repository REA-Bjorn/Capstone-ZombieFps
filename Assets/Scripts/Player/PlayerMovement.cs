using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movement;

    float sprint = 1.0f;

    [SerializeField] CharacterController charController;

    private void OnDisable()
    {
        charController.enabled = false;
    }

    private void OnEnable()
    {
        charController.enabled = true;  
    }

    public void Movement()
    {
        if (PlayerBase.instance.Health.IsValid)
        {
            if (InputManager.Instance.SprintON)
            {
                sprint = 1.5f;
            }
            else if(InputManager.Instance.SprintOff)
            {
                sprint = 1.0f;
            }
            movement = (transform.right * InputManager.Instance.MoveVect.x) + (transform.forward * InputManager.Instance.MoveVect.y);
            charController.Move(movement * Time.deltaTime * PlayerBase.instance.Spd.CurrentValue * sprint);
            charController.Move(new Vector3(0, -9.81f * Time.deltaTime, 0));
        }
    }

}
