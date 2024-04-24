using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;

    [SerializeField] private PointsPool points;
    [SerializeField] private CustomTimer doublePointsTimer;

    public event Action OnPointsChanged;
    public float CurrPts => points.CurrentValue;

    private float totalPoints = 0;
    public float TotalPts => totalPoints;
    
    private int mult = 1;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPoints(float val)
    {
        points.Increase(mult * val);
        totalPoints += (mult * val);
        OnPointsChanged?.Invoke();
    }

    public void RemovePoints(float val)
    {
        points.Decrease(val);
        OnPointsChanged?.Invoke();
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
