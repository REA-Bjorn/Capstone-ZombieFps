using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickup : BasePickup
{
    public override void Pickup()
    {
        WaveManager.Instance.KillAllAliveEnemies();
    }
}
