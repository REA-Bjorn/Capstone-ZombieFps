using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBatcher : MonoBehaviour
{
    private void Awake()
    {
        StaticBatchingUtility.Combine(gameObject);
    }
}
