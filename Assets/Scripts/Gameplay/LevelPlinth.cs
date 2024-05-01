using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomTimer))]
public class LevelPlinth : MonoBehaviour, IDamage
{
    [SerializeField] private string sceneName;
    [SerializeField] private MeshRenderer model;
    [SerializeField] private Material redMat;
    [SerializeField] private Material greenMat;
    [SerializeField] private CustomTimer unconfirmTimer;

    private bool confirming = false;

    public void TakeDamage(float damage, bool forceKilled = false)
    {
        if (confirming)
        {
            InputManager.Instance.PauseActions();
            if (sceneName == "QUIT")
            {
                Application.Quit();
            }
            else
            {
                UIManager.Instance.SceneFade.FadeTo(sceneName);
            }
        }
        else if (!unconfirmTimer.RunTimer)
        {
            unconfirmTimer.StartTimer();
        }
    }

    private void Start()
    {
        unconfirmTimer.OnEnd += () =>
        {
            confirming = false;
            model.material = redMat;
        };

        unconfirmTimer.OnStart += () =>
        {

            confirming = true;
            model.material = greenMat;
        };
    }

    private void OnDestroy()
    {
        unconfirmTimer.OnEnd -= () =>
        {
            confirming = false;
            model.material = redMat;
        };

        unconfirmTimer.OnStart -= () =>
        {

            confirming = true;
            model.material = greenMat;
        };
    }

    public void TakeMaxDamage()
    {
        // nah
    }
}
