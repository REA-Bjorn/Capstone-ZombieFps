using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movement;
    float sprint = 1.0f;

    [SerializeField] private CharacterController charController;
    [Seperator]
    [SerializeField] private AudioSource movementAudio;
    [Seperator]
    [SerializeField] private CustomTimer staminaRecharge;
    [SerializeField] private CustomTimer staminaDelay;

    private void OnDisable()
    {
        charController.enabled = false;
    }

    private void OnEnable()
    {
        charController.enabled = true;
    }

    public void Start()
    {
        staminaRecharge.OnTick += RechargeStamina_OnTick;
        staminaRecharge.OnEnd += RechargeStamina_OnEnd;
        staminaDelay.OnEnd += DelayStamina;
    }

    private void RechargeStamina_OnEnd()
    {
        PlayerBase.instance.Stam.SetMax();
    }

    private void RechargeStamina_OnTick()
    {
        if (!PlayerBase.instance.Stam.IsMaxed)
        {
            Debug.Log("Increasing!");
            PlayerBase.instance.Stam.Increase(Time.deltaTime);
        }
        else
        {
            // Manually stop the timer cause we are at max stamina6
            // and we should call the OnEnd func()
            staminaRecharge.StopTimer();
            RechargeStamina_OnEnd();
        }
    }

    private void DelayStamina()
    {
        staminaRecharge.StartTimer();
        Debug.Log(staminaRecharge.RunTimer);
    }

    public void Movement()
    {
        if (PlayerBase.instance.Health.IsValid && InputManager.Instance.PlayerMoved)
        {
            if (InputManager.Instance.SprintON && PlayerBase.instance.Stam.IsValid)
            {
                PlayerBase.instance.Stam.Decrease(Time.deltaTime);
                staminaDelay.StopTimer();
                staminaRecharge.StopTimer();
                movementAudio.pitch = 2f;
                sprint = 1.5f;
            }
            else if (InputManager.Instance.SprintOff)
            {
                staminaDelay.StartTimer();
                movementAudio.pitch = 1.2f;
                sprint = 1.0f;
            }
            else if (PlayerBase.instance.Stam.CurrentValue == 0)
            {
                movementAudio.pitch = 1.2f;
                sprint = 1.0f;
            }

            if (!movementAudio.isPlaying)
                movementAudio.Play();

            movement = (transform.right * InputManager.Instance.MoveVect.x) + (transform.forward * InputManager.Instance.MoveVect.y);
            charController.Move(movement * Time.deltaTime * PlayerBase.instance.Spd.CurrentValue * sprint);
            charController.Move(new Vector3(0, -9.81f * Time.deltaTime, 0));
        }
        else if (movementAudio.isPlaying)
        {
            movementAudio.Stop();
        }
    }

}
