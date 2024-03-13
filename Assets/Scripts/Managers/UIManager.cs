using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject death;

    private void Awake()
    {
        Instance = this;
    }

    public void DeathMenu()
    {
        death.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        death.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
