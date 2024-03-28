using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;

    public PointsPool points;

    private void Awake()
    {
        instance = this;
    }

    public void AddPoints(float val)
    {
        points.Increase(val);
    }

    public void RemovePoints(float val)
    {
        points.Decrease(val);
    }

    public float GetPoints()
    {
        return points.CurrentValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
