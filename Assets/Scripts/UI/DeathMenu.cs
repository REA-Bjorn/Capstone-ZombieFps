using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private Button retry;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private TextMeshProUGUI totalScoreDisplay;

    void Start()
    {
        retry.onClick.AddListener(GameManager.Instance.RetryLevel);
        mainMenuBtn.onClick.AddListener(() => { GameManager.Instance.LoadLevel(Levels.MainMenu); });
    }

    private void OnDisable()
    {
        retry.onClick.RemoveListener(GameManager.Instance.RetryLevel);
        mainMenuBtn.onClick.RemoveListener(() => { GameManager.Instance.LoadLevel(Levels.MainMenu); });
    }

    public void UpdateTotalScore()
    {
        totalScoreDisplay.text = PointsManager.Instance.TotalPts.ToString();
    }
}
