using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickup : BasePickup
{
    public override void Pickup()
    {
        Debug.Log("Nuke Picked Up");
        WaveManager.Instance.KillAllAliveEnemies();
    }
}
