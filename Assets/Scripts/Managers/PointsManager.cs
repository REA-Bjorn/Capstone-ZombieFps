using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;

    public event Action OnPointsChanged;

    [SerializeField] private PointsPool points;

    public float CurrPts => points.CurrentValue;

    [SerializeField] private CustomTimer doublePointsTimer;

    private int mult = 1;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPoints(float val)
    {
        points.Increase(mult * val);
        OnPointsChanged?.Invoke();
        UIManager.Instance.UpdateScore();
    }

    public void RemovePoints(float val)
    {
        points.Decrease(val);
        OnPointsChanged?.Invoke();
        UIManager.Instance.UpdateScore();
    }

    public float GetPoints()
    {
        return points.CurrentValue;
    }

    void Start()
    {
        doublePointsTimer.OnStart += DoublePointsTimer_OnStart;
        doublePointsTimer.OnEnd += DoublePointsTimer_OnEnd;
    }

    private void OnDestroy()
    {
        doublePointsTimer.OnStart -= DoublePointsTimer_OnStart;
        doublePointsTimer.OnEnd -= DoublePointsTimer_OnEnd;
    }

    private void DoublePointsTimer_OnEnd()
    {
        TurnOffDoublePoints();
    }

    private void DoublePointsTimer_OnStart()
    {
        mult = 2;
    }

    public void TurnOffDoublePoints()
    {
        mult = 1;
    }

    public void EnableDoublePoints()
    {
        doublePointsTimer.StartTimer();
    }
}
