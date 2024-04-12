using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private void Update()
    {
        Vector3 dir = PlayerBase.instance.transform.position - transform.position;
        dir.y = 0;
        dir.Normalize();

        transform.localRotation = Quaternion.LookRotation(-dir);
    }
}
