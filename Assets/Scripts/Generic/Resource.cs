using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    //Events
    public event System.Action OnDepleted;
    public event System.Action OnFilled;
    public event System.Action OnIncrease;
    public event System.Action OnDecrease;
    public event System.Action OnChanged;
    //Values
    [SerializeField] float max;
    [SerializeField] float min;
    [SerializeField] float currentValue;
    bool isEmpty;
    //Properties
    public float CurrentValue => currentValue;
    public float Percent => currentValue / max;
    public float Max => max;
    public float Min => min;
    public bool IsValid => currentValue > 0;
    public bool IsMaxed => currentValue >= max;

    public void Increase(float value)
    {
        float newValue = currentValue + value;
        if (max != 0)
        {
            newValue = Mathf.Max(newValue, max);
        }
        currentValue = newValue;
        OnIncrease?.Invoke();
        OnChanged?.Invoke();
        isEmpty = false;
    }

    public void Decrease(float value)
    {
        if (isEmpty)
        {
            return;
        }

        float newValue = Mathf.Max(currentValue - value, min);
        currentValue = newValue;
        OnDecrease?.Invoke();
        OnChanged?.Invoke();

        if (currentValue == 0)
        {
            OnDepleted?.Invoke();
            isEmpty = true;
        }
    }

    public void SetMax()
    {
        currentValue = max;
        OnFilled?.Invoke();
        OnChanged?.Invoke();
        isEmpty = false;
    }
}
