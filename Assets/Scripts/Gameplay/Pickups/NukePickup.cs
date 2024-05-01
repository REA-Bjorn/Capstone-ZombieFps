using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickup : BasePickup
{
    public override void Pickup()
    {
        int totalAlive = WaveManager.Instance.TotalCurrentlyUndead();

        PointsManager.Instance.AddPoints(Mathf.Max(totalAlive * 50, 200));
        
        WaveManager.Instance.KillAllAliveEnemies();
    }
}
