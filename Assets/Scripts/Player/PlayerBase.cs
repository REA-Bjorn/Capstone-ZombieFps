using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] HealthPool health;



    // Start is called before the first frame update
    void Start()
    {
        health.SetMax();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
