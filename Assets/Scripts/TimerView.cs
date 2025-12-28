using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TimerView : MonoBehaviour
{
    public event Action<ITimerInstance> RunButtonPressed;
    public event Action<ITimerInstance> StopButtonPressed;
    public event Action<ITimerInstance> ResumeButtonPressed;
    public event Action<ITimerInstance> ResetButtonPressed;

    public float GoalTime => _goalTime;
    
    [SerializeField] protected float _goalTime;
    [SerializeField] protected Button _startButton;
    [SerializeField] protected Button _resetButton;
    [SerializeField] protected Button _stoptButton;
    [SerializeField] protected Button _resumeButton;
    
    
    protected Coroutine _timerRoutine;

    private ITimerInstance _attachedTimer;
    
    private void Awake()
    {
        _straightTimer = new SimpleTimer(_goalTime);
        _straightTimer.CurrentTimeReported += OnStraightTimeReported;
        _straightTimer.GoalTimeReached += OnGoalTimeReached(ref _straightTimerRoutine);
        
        _countdownTimer = new CountdownTimer(_goalTime);
        _countdownTimer.CurrentTimeReported += OnCountdownTimeReported;
        _countdownTimer.GoalTimeReached += OnCountdownGoalTimeReached;
        
        _straightTimeProgressView.maxValue = _goalTime;
        SubscribeButtons();
    }

    private void OnDisable()
    {
        UnsubscribeButtons();
        ClearSecondsVisualizationList();
    }

    
    
    private void StartCountdownTimer()
    {
        VisualiseGoalTime();
        _countdownTimerRoutine = StartCoroutine(_straightTimer.StartCountdown());
    }
    
    private void OnStraightTimeReported(float time)
    {
        _timeDisplay.text = Mathf.FloorToInt(time).ToString();
        _straightTimeProgressView.value = time;
    }
    
    private void OnCountdownTimeReported(float time)
    {
        if (_visualisedSeconds != null && _visualisedSeconds.Count > time)
        {
            var currentSecond = _visualisedSeconds.Dequeue();
            Destroy(currentSecond.gameObject);
        }
    }
    
    private void OnGoalTimeReached(ref Coroutine routine)
    {
        if (_straightTimerRoutine != null)
        {
            StopCoroutine(_straightTimerRoutine);
            _straightTimerRoutine = null;
            Debug.Log($"Straight Timer Stopped at time {time}");
            Reset();
        }
    }
    
    private void OnCountdownGoalTimeReached(int time)
    {
        if (_countdownTimerRoutine != null)
        {
            StopCoroutine(_countdownTimerRoutine);
            _countdownTimerRoutine = null;
            Debug.Log($"Countdown Timer Stopped at time {time}");
            Reset();
        }
    }

    public void InjectTimer(Timer timer)
    {
        _attachedTimer = timer;
    }
    
    protected void Run()
    {
       RunButtonPressed?.Invoke(_attachedTimer);
    }
    
    protected void Stop()
    {
       StopButtonPressed?.Invoke(_attachedTimer);
    }
    
    protected void Resume()
    {
        ResumeButtonPressed?.Invoke(_attachedTimer);
    }
    
    protected void Reset()
    {
        ResetButtonPressed?.Invoke(_attachedTimer);
    }
    
    protected void SubscribeButtons()
    {
    _startButton.onClick.AddListener(Run);
    _resetButton.onClick.AddListener(Reset);
    _resumeButton.onClick.AddListener(Resume);
    _stoptButton.onClick.AddListener(Stop);
    
    }

    protected void UnsubscribeButtons()
    {
        _startButton.onClick.RemoveListener(Run);
        _resetButton.onClick.RemoveListener(Reset);
        _resumeButton.onClick.RemoveListener(Resume);
        _stoptButton.onClick.RemoveListener(Stop);
    }
}