using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotTree : MonoBehaviour
{
    public void InspectorButton()
    {
#if UNITY_EDITOR
        // safety check for trees game object in heirarchy
        if (transform.name == "Trees")
        {
            foreach (Transform t in transform)
            {
                float rY = Random.Range(0f, 360f);
                t.localRotation = Quaternion.Euler(0, rY, 0);
            }
        }
#endif
    }
}
