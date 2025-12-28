using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TimerView : MonoBehaviour
{
    [SerializeField] private float _elapsedTime;
    [SerializeField] private float _maxTime;
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
    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_elapsedTime, _maxTime);
        _timer.StraightTimeReported += OnStraightTimeReported;
        _timer.CountdownTimeReported += OnCountdownTimeReported;
        _timer.MaxTimeReached += OnMaxTimeReached;
        _timer.ElapsedTimeReached += OnCountdownTimeReached;
        _straightTimeProgressView.maxValue = _maxTime;
        SubscribeButtons();
    }

    private void OnDisable()
    {
        UnsubscribeButtons();
        ClearSecondsVisualizationList();
    }

    private void StartTimer()
    {
        if (_straightTimerRoutine != null || _countdownTimerRoutine != null)
            return;
        
        _straightTimerRoutine = StartCoroutine(_timer.StartCount());
        StartCountdownTimer();
    }
    
    private void StartCountdownTimer()
    {
        for (int i = 0; i < _maxTime; i++)
        {
            var secondVisual = Object.Instantiate(_secondVisualPrefab, _elapsedTimeProgressView);
            _visualisedSeconds.Enqueue(secondVisual);
        }

        _countdownTimerRoutine = StartCoroutine(_timer.StartCountdown());
    }
    
    private void ResetTimer()
    {
        OnMaxTimeReached(_timer.PassedTime);
        OnCountdownTimeReached(0);
        OnStraightTimeReported(0f);
        ClearSecondsVisualizationList();
        _timer.Reset();
    }
    
    private void OnStraightTimeReported(float passedTime)
    {
        _timeDisplay.text = Mathf.FloorToInt(passedTime).ToString();
        _straightTimeProgressView.value = passedTime;
    }
    
    private void OnCountdownTimeReported(int obj)
    {
        var currentSecond = _visualisedSeconds.Dequeue();
        Destroy(currentSecond.gameObject);
    }
    
    private void OnMaxTimeReached(float time)
    {
        if (_straightTimerRoutine != null)
        {
            StopCoroutine(_straightTimerRoutine);
            _straightTimerRoutine = null;
            Debug.Log($"Straight Timer Stopped at time {time}");
            ResetTimer();
        }
    }
    
    private void OnCountdownTimeReached(int time)
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
        _timer.Resume();
    }

    private void StopTimer()
    {
        _timer.Stop();
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
    _startButton.onClick.AddListener(StartTimer);
    _resetButton.onClick.AddListener(ResetTimer);
    _resumeButton.onClick.AddListener(ResumeTimer);
    _stoptButton.onClick.AddListener(StopTimer);
    
    }

    private void UnsubscribeButtons()
    {
        _startButton.onClick.RemoveListener(StartTimer);
        _resetButton.onClick.RemoveListener(ResetTimer);
        _resumeButton.onClick.RemoveListener(ResumeTimer);
        _stoptButton.onClick.RemoveListener(StopTimer);
    }
}