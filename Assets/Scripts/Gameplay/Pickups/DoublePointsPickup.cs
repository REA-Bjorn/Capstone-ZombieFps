using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointsPickup : BasePickup
{
    public override void Pickup()
    {
        Debug.Log("Double Points Pickup Up");
        // PointsManager.Instance.EnableDoublePoints();
    }
}
