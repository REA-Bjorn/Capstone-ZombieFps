using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject death;
    [SerializeField] GameState gameState;
    [SerializeField] WeaponUI weaponUIScript;

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

    public void UpdateWeaponsUI()
    {
        weaponUIScript.UpdateUI();
    }

    public IEnumerator FlashWeaponsUI()
    {
        weaponUIScript.ColorAmmo(Color.red);
        yield return new WaitForSeconds(0.25f);
        weaponUIScript.ColorAmmo(Color.white);
    }
}
