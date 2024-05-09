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
    [SerializeField] private TextMeshProUGUI totalWavesDisplay;

    void Start()
    {
        retry.onClick.AddListener(GameManager.Instance.RetryLevel);
        mainMenuBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.SceneFade.FadeTo(GameManager.KEY_MAINMENU);
        });
    }

    private void OnDisable()
    {
        retry.onClick.RemoveListener(GameManager.Instance.RetryLevel);
        mainMenuBtn.onClick.RemoveListener(() =>
        {
            UIManager.Instance.SceneFade.FadeTo(GameManager.KEY_MAINMENU);
        });
    }

    public void UpdateTotalScore()
    {
        totalScoreDisplay.text = PointsManager.Instance.TotalPts.ToString();
        totalWavesDisplay.text = WaveManager.Instance.CurrentWave;
    }
}
