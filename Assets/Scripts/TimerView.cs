using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TimerView : MonoBehaviour
{
    [SerializeField] private float _goalTime;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _stoptButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Slider _straightTimeProgressView;
    [SerializeField] private RectTransform _elapsedTimeProgressView;
    [SerializeField] private Image _secondVisualPrefab;
    [SerializeField] private TMP_Text _timeDisplay;
    
    private Queue<Image> _visualisedSeconds = new();
    
    private Coroutine _straightTimerRoutine;
    private Coroutine _countdownTimerRoutine;
    
    private Timer _straightTimer;
    private Timer _countdownTimer;

    private void Awake()
    {
        _straightTimer = new StraightTimer(_goalTime);
        _straightTimer.CurrentTimeReported += OnStraightTimeReported;
        _straightTimer.GoalTimeReached += OnGoalTimeReached;
        
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

    private void StartTimers()
    {
        if (_straightTimerRoutine != null || _countdownTimerRoutine != null)
            return;
        
        _straightTimerRoutine = StartCoroutine(_straightTimer.Start());
        _countdownTimerRoutine = StartCoroutine(_countdownTimer.Start());
    }
    
    private void StartCountdownTimer()
    {
        for (int i = 0; i < _goalTime; i++)
        {
            var secondVisual = Object.Instantiate(_secondVisualPrefab, _elapsedTimeProgressView);
            _visualisedSeconds.Enqueue(secondVisual);
        }

        _countdownTimerRoutine = StartCoroutine(_straightTimer.StartCountdown());
    }
    
    private void ResetTimer()
    {
        OnGoalTimeReached(_straightTimer.CurrentTime);
        OnCountdownGoalTimeReached(0);
        OnStraightTimeReported(0f);
        ClearSecondsVisualizationList();
        _straightTimer.Reset();
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
    
    private void OnGoalTimeReached(int time)
    {
        if (_straightTimerRoutine != null)
        {
            StopCoroutine(_straightTimerRoutine);
            _straightTimerRoutine = null;
            Debug.Log($"Straight Timer Stopped at time {time}");
            ResetTimer();
        }
    }
    
    private void OnCountdownGoalTimeReached(int time)
    {
        if (_countdownTimerRoutine != null)
        {
            StopCoroutine(_countdownTimerRoutine);
            _countdownTimerRoutine = null;
            Debug.Log($"Countdown Timer Stopped at time {time}");
            ResetTimer();
        }
    }
    
    private void ResumeTimer()
    {
        _straightTimer.Resume();
    }

    private void StopTimer()
    {
        _straightTimer.Stop();
    }

    private void ClearSecondsVisualizationList()
    {
        foreach (Image second in _visualisedSeconds)
        {
            Destroy(second.gameObject);
        }

        _visualisedSeconds.Clear();
    }
    
    private void SubscribeButtons()
    {
    _startButton.onClick.AddListener(StartTimers);
    _resetButton.onClick.AddListener(ResetTimer);
    _resumeButton.onClick.AddListener(ResumeTimer);
    _stoptButton.onClick.AddListener(StopTimer);
    
    }

    private void UnsubscribeButtons()
    {
        _startButton.onClick.RemoveListener(StartTimers);
        _resetButton.onClick.RemoveListener(ResetTimer);
        _resumeButton.onClick.RemoveListener(ResumeTimer);
        _stoptButton.onClick.RemoveListener(StopTimer);
    }
}