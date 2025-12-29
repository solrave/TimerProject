using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TimeCounter
{
    public event Action<float> CurrentTimeReported;
    public event Action<float> GoalTimeReached;
    public float CurrentTime => _currentTime;

    private readonly TimeView _view;
    protected float _goalTime;
    protected float _currentTime = 0f;
    
    protected bool _timerStopped;

    protected TimeCounter(TimeView view)
    {
        _view = view;
        _goalTime = _view.GoalTime;
    }

    public abstract IEnumerator Start();

    public abstract void Reset();

    public void Stop()
    {
        _timerStopped = true;
    }

    public void Resume()
    {
        _timerStopped = false;
    }

    protected virtual void OnCurrentTimeReported(float time)
    {
        CurrentTimeReported?.Invoke(time);
    }
    
    protected virtual void OnGoalTimeReached(int time)
    {
        GoalTimeReached?.Invoke(time);
    }
}
