using System.Collections;
using UnityEngine;

public class Stopwatch : TimeCounter
{
    public Stopwatch(TimeView view) : base(view)
    {
    }

    public override IEnumerator Start()
    {
        _timerStopped = false;
        while (_currentTime < _goalTime)
        {
            if (_timerStopped)
                yield return new WaitUntil(() => !_timerStopped);
            
            _currentTime += Time.deltaTime;
            OnCurrentTimeReported(_currentTime);
            yield return null;
        }

        //_currentTime = _goalTime;
        OnGoalTimeReached(Mathf.FloorToInt(_currentTime));
        Reset();
    }

    public override void Reset()
    {
        _currentTime = 0f;
        _timerStopped = true;
    }
}