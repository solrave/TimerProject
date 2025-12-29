using System.Collections;
using UnityEngine;

public class Timer : TimeCounter
{
    private float _currentSecond;
    private float _lastSecond;
    public Timer(TimeView view) : base(view)
    {
        _currentTime = _goalTime;
    }

    public override IEnumerator Start()
    {
        _timerStopped = false;
    
        while (_currentTime > 0f)
        {
            if (_timerStopped)
                yield return new WaitUntil(() => !_timerStopped);
        
            _currentTime -= Time.deltaTime;
            _currentSecond = Mathf.CeilToInt(_currentTime);
        
            if (_currentSecond < _lastSecond)
            {
                _lastSecond = _currentSecond;
                OnCurrentTimeReported(_currentSecond);
                yield return null;
            }
            yield return null;
        }
    
        OnGoalTimeReached(Mathf.CeilToInt(base._currentTime));
        Reset();
    }

    public override void Reset()
    {
        _currentTime = _goalTime;
        _timerStopped = true;
    }
}