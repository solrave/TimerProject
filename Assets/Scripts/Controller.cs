using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Controller : MonoBehaviour
{
    public event Action RunButtonPressed;
    public event Action StopButtonPressed;
    public event Action ResumeButtonPressed;
    public event Action ResetButtonPressed;

    public float GoalTime => _goalTime;

    [SerializeField] private GameObject _forwardTimePrefab;
    [SerializeField] private GameObject _countdownTimePrefab;

    [SerializeField] private Transform _visualHolder;
    
    [SerializeField] protected float _goalTime;
    [SerializeField] private bool _countdownTimer;
    
    [SerializeField] protected Button _startButton;
    [SerializeField] protected Button _resetButton;
    [SerializeField] protected Button _stopButton;
    [SerializeField] protected Button _resumeButton;

    private Coroutine _routine;
    private Timer _timer;
   
    private VisualComponent _currentVisual;
    
    protected void OnDisable()
    {
        UnsubscribeButtons();
        UnsubscribeTimeCounter();
    }

    public void OnEnable()
    {
        _timer = new Timer(_goalTime);
        
        if (_countdownTimer)
        {
            _currentVisual = GameObject.Instantiate(_countdownTimePrefab, _visualHolder).GetComponent<VisualComponent>();
            
        }
        else
        {
            _currentVisual = GameObject.Instantiate(_forwardTimePrefab, _visualHolder).GetComponent<VisualComponent>();
            
        }

        if (_currentVisual is not null) _currentVisual.InitVisual(_timer);
        
        SubscribeButtons();
        SubscribeTimeCounter();
    }

    private void OnTimeChanged(float time)
    {
        
    }
    
    private void OnGoalTimeReached(float time)
    {
        Reset();
    }
    
    private void RunTimer()
    {
        if (_routine != null) return;

        _routine = StartCoroutine(_timer.Start());
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
    
    private void Reset()
    {
        if (_routine != null) StopCoroutine(_routine);

        _routine = null;
        _timer.Reset();
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
        _timer.CurrentTimeUpdated += OnTimeChanged;
        _timer.GoalTimeReached += OnGoalTimeReached;
        RunButtonPressed += RunTimer;
        StopButtonPressed += _timer.Stop;
        ResumeButtonPressed += _timer.Resume;
        ResetButtonPressed += Reset;
        ResetButtonPressed += _currentVisual.ResetVisual;
    }

    private void UnsubscribeTimeCounter()
    {
        _timer.CurrentTimeUpdated -= OnTimeChanged;
        _timer.GoalTimeReached -= OnGoalTimeReached;
        RunButtonPressed -= RunTimer;
        StopButtonPressed -= _timer.Stop;
        ResumeButtonPressed -= _timer.Resume;
        ResetButtonPressed -= _timer.Reset;
        ResetButtonPressed -= _currentVisual.ResetVisual;
    }
}