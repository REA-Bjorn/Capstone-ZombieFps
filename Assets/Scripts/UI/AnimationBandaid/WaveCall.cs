using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCall : MonoBehaviour
{
    public void UpdateCounter()
    {
        WaveManager.Instance.IncWave();
    }
}
