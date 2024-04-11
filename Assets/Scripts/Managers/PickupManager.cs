using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager Instance;
    [SerializeField] private List<GameObject> pickups;

    private void Awake()
    {
        Instance = this;
    }

    public void DropPickup(Transform _position)
    {
        if (pickups.Count == 0)
        {
            return;
        }

        float randomNumber = Random.Range(0.0f, 100.0f);

        if (randomNumber >= 75.0f)
        {
            GameObject pickup = pickups[Random.Range(0, pickups.Count)];
            Vector3 pos = new Vector3(_position.position.x, _position.position.y + 1, _position.position.z);
            Instantiate(pickup, pos, _position.rotation);
        }
    }

}
