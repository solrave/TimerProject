using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public abstract class TimeView : MonoBehaviour
{
    public event Action RunButtonPressed;
    public event Action StopButtonPressed;
    public event Action ResumeButtonPressed;
    public event Action ResetButtonPressed;

    public float GoalTime => _goalTime;
    
    [SerializeField] protected float _goalTime;
    [SerializeField] protected Button _startButton;
    [SerializeField] protected Button _resetButton;
    [SerializeField] protected Button _stoptButton;
    [SerializeField] protected Button _resumeButton;
    
    protected Coroutine _routine;
    protected TimeCounter _timeCounter;
    
    protected virtual void OnEnable()
    {
        SubscribeButtons();
        SubscribeTimeCounter();
    }
    
    protected void OnDisable()
    {
        UnsubscribeButtons();
        UnsubscribeTimeCounter();
    }

    public void InjectTimeCounter(TimeCounter timeCounter)
    {
        _timeCounter = timeCounter;
    }
    
    protected abstract void OnTimeChanged(float time);

    protected void OnGoalTimeReached(float time)
    {
        ResetView();
    }
    
    protected void OnRunPressed()
    {
       RunButtonPressed?.Invoke();
    }
    protected void OnStopPressed()
    {
       StopButtonPressed?.Invoke();
    }
    protected void OnResumePressed()
    {
        ResumeButtonPressed?.Invoke();
    }
    protected void OnResetPressed()
    {
        ResetButtonPressed?.Invoke();
    }
    protected void SubscribeButtons()
    {
    _startButton.onClick.AddListener(OnRunPressed);
    _resetButton.onClick.AddListener(OnResetPressed);
    _resumeButton.onClick.AddListener(OnResumePressed);
    _stoptButton.onClick.AddListener(OnStopPressed);
    
    }
    protected void UnsubscribeButtons()
    {
        _startButton.onClick.RemoveListener(OnRunPressed);
        _resetButton.onClick.RemoveListener(OnResetPressed);
        _resumeButton.onClick.RemoveListener(OnResumePressed);
        _stoptButton.onClick.RemoveListener(OnStopPressed);
    }
    
    protected void SubscribeTimeCounter()
    {
        _timeCounter.CurrentTimeReported += OnTimeChanged;
        _timeCounter.GoalTimeReached += OnGoalTimeReached;
        RunButtonPressed += RunTimeCounter;
        StopButtonPressed += _timeCounter.Stop;
        ResumeButtonPressed += _timeCounter.Resume;
        ResetButtonPressed += ResetView;
    }

    protected void UnsubscribeTimeCounter()
    {
        _timeCounter.CurrentTimeReported -= OnTimeChanged;
        _timeCounter.GoalTimeReached -= OnGoalTimeReached;
        RunButtonPressed -= RunTimeCounter;
        StopButtonPressed -= _timeCounter.Stop;
        ResumeButtonPressed -= _timeCounter.Resume;
        ResetButtonPressed -= _timeCounter.Reset;
    }
    
    protected virtual void RunTimeCounter()
    {
        if (_routine != null) return;

        _routine = StartCoroutine(_timeCounter.Start());
    }
    
    protected virtual void ResetView()
    {
        if (_routine != null) StopCoroutine(_routine);

        _routine = null;
    }
}
