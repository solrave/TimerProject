using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Timer
{
    public event Action<float> StraightTimeReported;
    public event Action<int> CountdownTimeReported; 
    public event Action<float> MaxTimeReached;
    public event Action<int> ElapsedTimeReached;
    public float PassedTime => _passedTime;
    
    private float _elapsedTime;
    private float _currentElapsedTime;
    private float _maxTime;
    private float _passedTime = 0f;
    private int _lastSecond;
    private int _currentSecond;
    private bool _timerStopped;
    
    public Timer(float elapsedTime, float maxTime)
    {
        _elapsedTime = elapsedTime;
        _currentElapsedTime = _elapsedTime;
        _maxTime = maxTime;
        _lastSecond = Mathf.CeilToInt(_elapsedTime);
    }

    public IEnumerator StartCount()
    {
        _timerStopped = false;
        while (_passedTime < _maxTime)
        {
            if (_timerStopped)
                yield return new WaitUntil(() => !_timerStopped);
            
            _passedTime += Time.deltaTime;
            StraightTimeReported?.Invoke(_passedTime);
            yield return null;
        }

        _passedTime = _maxTime;
        MaxTimeReached?.Invoke(_passedTime);
        _passedTime = 0f;
        _timerStopped = true;
    }
    
    public IEnumerator StartCountdown()
    {
        _timerStopped = false;
        
        while (_currentElapsedTime > 0f)
        {
            if (_timerStopped)
                yield return new WaitUntil(() => !_timerStopped);
            
            _currentElapsedTime -= Time.deltaTime;
            _currentSecond = Mathf.CeilToInt(_currentElapsedTime);
            
            if (_currentSecond < _lastSecond)
            {
                _lastSecond = _currentSecond;
                CountdownTimeReported?.Invoke(Mathf.CeilToInt(_currentSecond));
                yield return null;
            }
            yield return null;
        }
        
        ElapsedTimeReached?.Invoke(Mathf.CeilToInt(_passedTime));
        _currentElapsedTime = _elapsedTime;
        _timerStopped = true;
    }

    public void Reset()
    {
        _passedTime = 0f;
        _currentElapsedTime = _elapsedTime;
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