using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action<float> CurrentTimeReported;
    public event Action<float> GoalTimeReached;
    public float CurrentTime => _currentTime;

    protected float _goalTime;
    protected float _currentTime = 0f;
    
    protected bool _timerStopped;

    public Timer(float goalTime)
    {
        _goalTime = goalTime;
    }

    public  IEnumerator Start()
    {
        _timerStopped = false;
        while (_currentTime < _goalTime)
        {
            if (_timerStopped)
                yield return new WaitUntil(() => !_timerStopped);
            
            _currentTime += Time.deltaTime;
            CurrentTimeReported?.Invoke(_currentTime);
            yield return null;
        }
        GoalTimeReached?.Invoke(_currentTime);
        Reset();
    }

    public  void Reset()
     {
         _currentTime = 0f;
         _timerStopped = true;
     }

    public void Stop()
    {
        _timerStopped = true;
    }

    public void Resume()
    {
        _timerStopped = false;
    }
}
