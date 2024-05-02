using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class BlockadeManager : MonoBehaviour
{
    public static BlockadeManager Instance;
    public NavMeshSurface surface;

    private void Awake()
    {
        Instance = this;
    }

    [Serializable]
    public struct BlockadeGroup
    {
        public Blockade blockadeScript;
        public List<GameObject> spawnPoints;
    }

    public List<BlockadeGroup> levelBlockades;

    private void Start()
    {
        // On every level startup, sub each blockade to the function
        foreach (BlockadeGroup blkade in levelBlockades)
        {
            blkade.blockadeScript.OnSuccessfulInteract += BlockadeUnlocked;

            // Loop through all spawners under that one blockade
            foreach (GameObject spawner in blkade.spawnPoints)
            {
                spawner.SetActive(false);
            }
        }
    }

    private void BlockadeUnlocked()
    {
        foreach (BlockadeGroup blkade in levelBlockades)
        {
            // Check for the first bought blockade, (we remove it during this as well)
            if (blkade.blockadeScript.Bought)
            {
                // Unsub the event
                blkade.blockadeScript.OnSuccessfulInteract -= BlockadeUnlocked;

                // Loop through all spawners under that one blockade
                foreach (GameObject spawner in blkade.spawnPoints)
                {
                    spawner.SetActive(true);
                }

                // Remove the blockade since it will no longer be needed
                levelBlockades.Remove(blkade);
                return; // we don't need to check any more since this is the only one
            }
        }

    }
}
