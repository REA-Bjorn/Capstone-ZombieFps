using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeRocks : MonoBehaviour
{
    public void InspectorButton()
    {
#if UNITY_EDITOR
        // safety check for Rocks game object in heirarchy
        if (transform.name == "Rocks")
        {
            foreach (Transform t in transform)
            {
                float rX = Random.Range(0f, 360f);
                float rY = Random.Range(0f, 360f);
                float rZ = Random.Range(0f, 360f);
                t.localRotation = Quaternion.Euler(rX, rY, rZ);

                float sX = Random.Range(1.2f, 2f);
                float sY = Random.Range(1.2f, 2f);
                float sZ = Random.Range(1.2f, 2f);
                t.localScale = new Vector3(sX, sY, sZ);
            }
        }
#endif
    }
}
