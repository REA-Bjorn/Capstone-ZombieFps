using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] Button retry;

    // Start is called before the first frame update
    void Start()
    {
        retry.onClick.AddListener(GameManager.instance.RetryLevel);
    }

    private void OnDisable()
    {
        retry.onClick.RemoveListener(GameManager.instance.RetryLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
