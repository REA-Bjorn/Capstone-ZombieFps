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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Movement()
    {
        movement = (transform.right * InputManager.Instance.MoveVect.x) + (transform.forward * InputManager.Instance.MoveVect.y);

        charController.Move(movement * Time.deltaTime * PlayerBase.instance.Spd.CurrentValue);
    }

}
