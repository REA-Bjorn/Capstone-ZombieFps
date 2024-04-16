using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] protected float unlockCost = 500.0f;
    [SerializeField] protected TextMeshProUGUI costDisplay;

    public virtual bool Interact()
    {
        if (PointsManager.Instance.CurrPts >= unlockCost)
        {
            PointsManager.Instance.RemovePoints(unlockCost);
            return true;
        }

        return false;
    }

    public virtual void Start()
    {
        PointsManager.Instance.OnPointsChanged += UpdateTextColor;
        costDisplay.text = "Unlock: $" + unlockCost.ToString();
        UpdateTextColor();
    }

    public virtual void OnDestroy()
    {
        PointsManager.Instance.OnPointsChanged -= UpdateTextColor;
    }

    public virtual void UpdateTextColor()
    {
        if (costDisplay != null) // null check it
        {
            if (PointsManager.Instance.CurrPts >= unlockCost)
            {
                costDisplay.color = Color.green;
            }
            else
            {
                costDisplay.color = Color.red;
            }
        }
    }
}
