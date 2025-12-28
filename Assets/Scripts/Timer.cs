using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Timer
{
    public event Action<float> PassedTimeReported;
    public event Action<float> MaxTimeReached;
    public event Action<float> ElapsedTimeReached;
    public float PassedTime => _passedTime;
    
    private float _elapsedTime;
    private float _maxTime;
    private float _passedTime = 0f;
    private bool _timerStarted;
    private bool _timerReset;
    private bool _timerStopped;
    public Timer(float elapsedTime, float maxTime)
    {
        _elapsedTime = elapsedTime;
        _maxTime = maxTime;
    }

    public IEnumerator StartStraightTimer()
    {
        while (_passedTime < _maxTime)
        {
            _passedTime += Time.deltaTime;
            PassedTimeReported?.Invoke(_passedTime);
            yield return null;
        }

        _passedTime = _maxTime;
        MaxTimeReached?.Invoke(_passedTime);
        yield return null;
    }
    
    private IEnumerator StartCountdownTimer()
    {
        while (_elapsedTime > 0f)
        {
            _elapsedTime -= Time.deltaTime;
            PassedTimeReported?.Invoke(_passedTime);
            yield return null;
        }

        _elapsedTime = 0f;
        ElapsedTimeReached?.Invoke(_elapsedTime);
        yield return null;
    }
    public void ResetTimer(){}
    public void StopTimer(){}
    public void ResumeTimer(){}

    
}