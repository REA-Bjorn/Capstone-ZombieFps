using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject death;
    [SerializeField] GameState gameState;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore()
    {
        gameState.UpdateScore();
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
