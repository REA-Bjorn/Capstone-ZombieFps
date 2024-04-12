using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] Button replay;

    // Start is called before the first frame update
    void OnEnable()
    {
        replay.onClick.AddListener(GameManager.instance.RetryLevel);
    }

    private void OnDisable()
    {
        replay.onClick.RemoveListener(GameManager.instance.RetryLevel);
    }
}
