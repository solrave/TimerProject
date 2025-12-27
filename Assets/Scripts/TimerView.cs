using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _stoptButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Slider _slider;
    [SerializeField] private RectTransform _countdownView;
    [SerializeField] private Image _secondVisualPrefab;
    [SerializeField] private int _elapsedTime;
    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_elapsedTime);
        _startButton.onClick.AddListener(StartTimer);
        _resetButton.onClick.AddListener(_timer.ResetTimer);
        _stoptButton.onClick.AddListener(_timer.StopTimer);
        _resumeButton.onClick.AddListener(_timer.ResumeTimer);
    }

    private void StartTimer()
    {
        _timer.StartTimer(Time.deltaTime);
    }
}