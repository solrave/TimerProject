using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimerView : TimerView
{
    [SerializeField] private RectTransform _timeProgressView;
    [SerializeField] private Image _secondVisualPrefab;
    private Queue<Image> _visualisedSeconds = new();
    
    private void OnEnable()
    {
        SubscribeButtons();
    }

    private void OnDisable()
    {
        UnsubscribeButtons();
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