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
    [SerializeField] private float sprintMoveBase = 1.0f;
    [SerializeField] private float sprintMoveInc = 1.5f;
    [SerializeField] private float sprintAudioIncPitch = 2.0f;
    [SerializeField] private float sprintAudioBasePitch = 1.2f;
    [Seperator]
    [SerializeField] private CustomTimer rechargeStamTimer;
    [SerializeField] private CustomTimer delayRechargeStamTimer;

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
        delayRechargeStamTimer.OnEnd += () =>
        {
            rechargeStamTimer.StartTimer();
        };

        rechargeStamTimer.OnTick += () =>
        {
            if (!PlayerBase.instance.Stam.IsMaxed)
            {
                PlayerBase.instance.Stam.Increase(Time.deltaTime);
            }
            else
            {
                rechargeStamTimer.StopTimer();
            }
        };

    }

    public void Movement()
    {
        if (PlayerBase.instance.Health.IsValid && InputManager.Instance.PlayerMoved)
        {
            StaminaCheck();

            if (!movementAudio.isPlaying)
                movementAudio.Play();

            // Find out what the modified value of speed is
            // Use the current move speed amount times the sprint modifier * if the move speed perk is unlocked then times 1.3
            float moveSpeedValModified = PlayerBase.instance.Spd.CurrentValue * sprint * (PerkManager.Instance.MoveSpeedSprite ? 1.3f : 1.0f);

            movement = (transform.right * InputManager.Instance.MoveVect.x) + (transform.forward * InputManager.Instance.MoveVect.y);
            charController.Move(movement * Time.deltaTime * moveSpeedValModified);
        }
        else if (movementAudio.isPlaying)
        {
            movementAudio.Stop();
        }

        charController.Move(new Vector3(0, (-9.81f * Time.deltaTime) * 2, 0));
    }

    private void StaminaCheck()
    {
        if (InputManager.Instance.SprintON && PlayerBase.instance.Stam.IsValid)
        {
            PlayerBase.instance.Stam.Decrease(Time.deltaTime);

            if (delayRechargeStamTimer.RunTimer)
            {
                delayRechargeStamTimer.StopTimer();
            }

            if (rechargeStamTimer.RunTimer)
            {
                rechargeStamTimer.StopTimer();
            }

            // Set values for sprint
            movementAudio.pitch = sprintAudioIncPitch;
            sprint = sprintMoveInc;
        }
        else if (InputManager.Instance.SprintOff)
        {
            if (!delayRechargeStamTimer.RunTimer)
            {
                delayRechargeStamTimer.StartTimer();
            }

            movementAudio.pitch = sprintAudioBasePitch;
            sprint = sprintMoveBase;
        }
        else if (PlayerBase.instance.Stam.CurrentValue == 0)
        {
            if (!delayRechargeStamTimer.RunTimer)
            {
                delayRechargeStamTimer.StartTimer();
            }

            movementAudio.pitch = sprintAudioBasePitch;
            sprint = sprintMoveBase;
        }
    }
}
