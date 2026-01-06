using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action<float> CurrentTimeUpdated;
    public event Action<int> CurrentSecondUpdated;
    public event Action TimerStarted;
    public event Action<float> GoalTimeReached;
    public float CurrentTime => _currentTime;
    public float GoalTime => _goalTime;

    private float _goalTime;
    private float _currentTime = 0f;

    private bool _timerStopped;
    private int _currentSecond;

    public Timer(float goalTime)
    {
        _goalTime = goalTime;
    }

    public  IEnumerator Start()
    {
        TimerStarted?.Invoke();
        _timerStopped = false;
        while (_currentTime < _goalTime)
        {
            if (_timerStopped)
                yield return new WaitUntil(() => !_timerStopped);
            
            _currentTime += Time.deltaTime;
            CurrentTimeUpdated?.Invoke(_currentTime);
            if ((int)_currentTime > _currentSecond)
            {
                _currentSecond = (int)_currentTime;
                CurrentSecondUpdated?.Invoke(_currentSecond);
            }
            yield return null;
        }
        GoalTimeReached?.Invoke(_currentTime);
        Reset();
    }

    public  void Reset()
     {
         _currentTime = 0f;
         _currentSecond = 0;
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
