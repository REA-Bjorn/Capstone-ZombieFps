using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CustomTimer))]
public class LevelPlinth : MonoBehaviour, IDamage
{
    [SerializeField] private string sceneName;
    [SerializeField] private string displayName;
    [SerializeField] private MeshRenderer model;
    [SerializeField] private Material redMat;
    [SerializeField] private Material greenMat;
    [SerializeField] private CustomTimer unconfirmTimer;
    [SerializeField] private TextMeshProUGUI text;

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
            if (sceneName == "QUIT")
                text.text = "Quit \n" + displayName;
            else
            {
                text.text = "Play \n" + displayName;
            }
        };

        unconfirmTimer.OnStart += () =>
        {
            confirming = true;
            model.material = greenMat;
            text.text = "Are you sure?";
        };
    }

    private void OnDestroy()
    {
        unconfirmTimer.OnEnd -= () =>
        {
            confirming = false;
            model.material = redMat;
            if (sceneName == "QUIT")
                text.text = "Quit \n" + displayName;
            else
            {
                text.text = "Play \n" + displayName;
            }
        };

        unconfirmTimer.OnStart -= () =>
        {

            confirming = true;
            model.material = greenMat;
            text.text = "Are you sure?";
        };
    }

    public void TakeMaxDamage()
    {
        // nah
    }
}
