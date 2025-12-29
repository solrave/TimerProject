using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : TimeView
{
    [SerializeField] private RectTransform _timeProgressView;
    [SerializeField] private Image _secondVisualPrefab;
    private Queue<Image> _visualisedSeconds = new();

    protected override void OnTimeChanged(float time)
    {
       
    }

    protected override void OnGoalTimeReached(float time)
    {
        
    }


    private void ClearSecondsVisualizationList()
    {
        foreach (Image second in _visualisedSeconds)
        {
            Destroy(second.gameObject);
        }

        _visualisedSeconds.Clear();
    }
}