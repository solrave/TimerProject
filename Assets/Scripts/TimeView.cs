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
    [SerializeField] protected Button _stopButton;
    [SerializeField] protected Button _resumeButton;

    private Coroutine _routine;
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
    protected abstract void ResetThisView();
    
    protected virtual void RunTimeCounter()
    {
        if (_routine != null) return;

        _routine = StartCoroutine(_timeCounter.Start());
    }

    private void OnGoalTimeReached(float time)
    {
        ResetView();
    }

    private void OnRunPressed()
    {
       RunButtonPressed?.Invoke();
    }

    private void OnStopPressed()
    {
       StopButtonPressed?.Invoke();
    }

    private void OnResumePressed()
    {
        ResumeButtonPressed?.Invoke();
    }

    private void OnResetPressed()
    {
        ResetButtonPressed?.Invoke();
    }

    private void SubscribeButtons()
    {
    _startButton.onClick.AddListener(OnRunPressed);
    _resetButton.onClick.AddListener(OnResetPressed);
    _resumeButton.onClick.AddListener(OnResumePressed);
    _stopButton.onClick.AddListener(OnStopPressed);
    
    }

    private void UnsubscribeButtons()
    {
        _startButton.onClick.RemoveListener(OnRunPressed);
        _resetButton.onClick.RemoveListener(OnResetPressed);
        _resumeButton.onClick.RemoveListener(OnResumePressed);
        _stopButton.onClick.RemoveListener(OnStopPressed);
    }

    private void SubscribeTimeCounter()
    {
        _timeCounter.CurrentTimeReported += OnTimeChanged;
        _timeCounter.GoalTimeReached += OnGoalTimeReached;
        RunButtonPressed += RunTimeCounter;
        StopButtonPressed += _timeCounter.Stop;
        ResumeButtonPressed += _timeCounter.Resume;
        ResetButtonPressed += ResetView;
    }

    private void UnsubscribeTimeCounter()
    {
        _timeCounter.CurrentTimeReported -= OnTimeChanged;
        _timeCounter.GoalTimeReached -= OnGoalTimeReached;
        RunButtonPressed -= RunTimeCounter;
        StopButtonPressed -= _timeCounter.Stop;
        ResumeButtonPressed -= _timeCounter.Resume;
        ResetButtonPressed -= _timeCounter.Reset;
    }
    
    private void ResetView()
    {
        if (_routine != null) StopCoroutine(_routine);

        _routine = null;
        ResetThisView();
        _timeCounter.Reset();
    }
}
