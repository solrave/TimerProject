using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForwardVisual : VisualComponent
{

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    private void OnDestroy()
    {
        _timer.CurrentTimeUpdated -= UpdateVisual;
    }

    public override void InitVisual(Timer timer)
    {
        base.InitVisual(timer);
        _slider.maxValue = _timer.GoalTime;
        _text.text = "0";
        _timer.CurrentTimeUpdated += UpdateVisual;
    }

    public override void UpdateVisual(float time)
    {
        _slider.value = time;
        _text.text = time.ToString();
    }

    public override void ResetVisual()
    {
        _slider.value = 0;
        _text.text = "0";
    }
}