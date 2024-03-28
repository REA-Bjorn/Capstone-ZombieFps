using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI total;

    public void UpdateScore()
    {
        total.text = PointsManager.instance.GetPoints().ToString();
    }
}
