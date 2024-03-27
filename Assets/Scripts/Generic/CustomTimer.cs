using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTimer : MonoBehaviour
{

    //Events
    public event System.Action OnEnd;
    public event System.Action OnStart;
    public event System.Action OnRestart;
    public event System.Action OnTick;

    //Values
    [SerializeField] float duration;
    private float currTime;
    public bool RunTimer = false;

    //Properties
    public float CurrentTime => currTime;
    public float DurationTime => duration;
    public float Percentage => Mathf.Clamp01(currTime/duration);
    public float ReversePercentage => Mathf.Clamp01((duration - currTime)/duration);


    public void SetToMax()
    {
        currTime = duration;
        RunTimer = true;
    }

    public void RestartTimer()
    {
        SetToMax();
        OnRestart?.Invoke();
    }

    public void StartTimer(float _duration = 0f)
    {
        duration = _duration;
        SetToMax();
        OnStart?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RunTimer)
        {
            if (currTime>0)
            {
                currTime -= Time.deltaTime;
                OnTick?.Invoke();
            }
            else
            {
                currTime = 0;
                OnEnd?.Invoke();
                RunTimer = false;
            }
        }
    }
}
