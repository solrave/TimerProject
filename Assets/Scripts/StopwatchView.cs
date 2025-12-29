using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StopwatchView : TimeView
{
    [SerializeField] private Slider _timeProgress;
    [SerializeField] private TMP_Text _timeDisplay;

    protected override void OnEnable()
    {
        base.OnEnable();
        _timeProgress.maxValue = _goalTime;
    }
    
    protected override void OnTimeChanged(float time)
    {
        _timeProgress.value = time;
        _timeDisplay.text = time.ToString();
    }

    protected override void ResetThisView()
    {
        _timeProgress.value = 0f;
        _timeDisplay.text = "0";
        Debug.Log($"Stopwatch finished at {_timeCounter.CurrentTime} !");
    }
}