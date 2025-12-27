using System;
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
    [SerializeField] private Slider _passedTimeView;
    [SerializeField] private RectTransform _elapsedTimeView;
    [SerializeField] private Image _secondVisualPrefab;
    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_elapsedTime);
        _startButton.onClick.AddListener(StartTimer);
        _resetButton.onClick.AddListener(_timer.ResetTimer);
        _stoptButton.onClick.AddListener(_timer.StopTimer);
        _resumeButton.onClick.AddListener(_timer.ResumeTimer);
    }

    private void Update()
    {
        
    }

    private void StartTimer()
    {
        _timer.StartTimer(Time.deltaTime);
    }
}