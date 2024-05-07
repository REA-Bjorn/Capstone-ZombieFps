using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager Instance;

    [SerializeField] private Volume PostFXVol;

    public void TogglePostProcess(bool _status)
    {
        PostFXVol.enabled = _status;
    }

    private void Awake()
    {
        Instance = this;
    }
}
