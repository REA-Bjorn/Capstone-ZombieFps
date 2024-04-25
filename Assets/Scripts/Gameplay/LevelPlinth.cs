using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlinth : MonoBehaviour, IDamage
{
    [SerializeField] private string sceneName;

    public void TakeDamage(float damage)
    {
        UIManager.Instance.SceneFade.FadeTo(sceneName);
    }

    public void TakeMaxDamage()
    {

    }
}
