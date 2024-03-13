using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject death;
    [SerializeField] GameObject win;
    [SerializeField] GameState gameState;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateSum()
    {
        gameState.UpdateScore();
    }

    public void WinMenu()
    {
        win.SetActive(true);
    }

    public void DeathMenu()
    {
        death.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        death.SetActive(false); 
        win.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
