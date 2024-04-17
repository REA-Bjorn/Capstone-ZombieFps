using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastInteractor : MonoBehaviour
{
    private RaycastHit hit;
    private bool displayingPopup;
    private IPopup storedPopup;

    private void Start()
    {
        InputManager.Instance.Action.Interact.started += InteractorStarted;
    }

    private void InteractorStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, 10))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (interactable.Interact())
                {
                    // show points being deducted on UI
                }
                else
                {
                    // turn points red on UI
                }
            }
        }
    }

    void Update()
    {
        CrosshairCheck();
        PopupAndInteractorUpdate();
    }

    private void CrosshairCheck()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, PlayerBase.instance.ShootDist) && hit.collider.CompareTag("Enemy"))
        {
            UIManager.Instance.UpdateCrosshair(Color.red);
        }
        else
        {
            UIManager.Instance.UpdateCrosshair(Color.white);
        }
    }

    private void PopupAndInteractorUpdate()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, 10))
        {
            IPopup popupHit = hit.collider.GetComponent<IPopup>();
            if (popupHit != null && !displayingPopup)
            {
                storedPopup = popupHit;
                displayingPopup = true;
                popupHit.DisplayInteractText();
            }
            else if (popupHit == null && storedPopup != null)
            {
                storedPopup.HideInteractText();
                displayingPopup = false;
            }
        }
        else if (displayingPopup)
        {
            storedPopup?.HideInteractText();
            displayingPopup = false;
        }
    }
}
