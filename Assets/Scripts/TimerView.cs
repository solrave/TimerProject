using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
    private Coroutine StraightTimerRoutine;
    private Coroutine CountdownTimerRoutine;
    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_elapsedTime, _maxTime);
        _timer.PassedTimeReported += DisplayReportedTime;
        _timer.MaxTimeReached += StopStraightTimer;
        _timer.ElapsedTimeReached += StopCountdownTimer;
        SubscribeButtons();
    }

    private void OnDisable()
    {
        UnsubscribeButtons();
    }
    
    private void DisplayReportedTime(float time)
    {
        _timeDisplay.text = time.ToString();
    }

    private void StartStraightTimer()
    {
        StraightTimerRoutine = StartCoroutine(_timer.StartStraightTimer());
        while (StraightTimerRoutine != null)
        {
            _straightTimeProgressView.value =
                Mathf.Lerp(_timer.PassedTime, _maxTime, _timer.PassedTime / _maxTime);
        }
    }
    
    private void ResetTimer(){}
    private void ResumeTimer(){}
    private void StopTimer(){}

    private void StopStraightTimer(float time)
    {
        if (StraightTimerRoutine != null)
        {
            StopCoroutine(StraightTimerRoutine);
            StraightTimerRoutine = null;
            Debug.Log($"Straight Timer Stopped at time {time}");
        }
    }
    
    private void StopCountdownTimer(float time){}

    private void SubscribeButtons()
    {
    _startButton.onClick.AddListener(StartStraightTimer);
    _resetButton.onClick.AddListener(ResetTimer);
    _resumeButton.onClick.AddListener(ResumeTimer);
    _stoptButton.onClick.AddListener(StopTimer);
    
    }

    private void UnsubscribeButtons()
    {
        _startButton.onClick.RemoveListener(StartStraightTimer);
        _resetButton.onClick.RemoveListener(ResetTimer);
        _resumeButton.onClick.RemoveListener(ResumeTimer);
        _stoptButton.onClick.RemoveListener(StopTimer);
    }
}