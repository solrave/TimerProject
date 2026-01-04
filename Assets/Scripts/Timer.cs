using System.Collections;
using UnityEngine;

public class Timer : TimeCounter
{
    private int _currentSecond;
    private int _lastSecond;
    public Timer(TimeView view) : base(view)
    {
        _currentTime = _goalTime;
        _lastSecond = Mathf.RoundToInt(_goalTime);
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
    
        OnGoalTimeReached(Mathf.CeilToInt(_currentTime));
        Reset();
    }

    public override void Reset()
    {
        _currentTime = _goalTime;
        _timerStopped = true;
    }
}