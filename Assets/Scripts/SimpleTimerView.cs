using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleTimerView : TimerView
{
    [SerializeField] private Slider _timeProgressView;
    [SerializeField] private TMP_Text _timeDisplay;
    
    private void OnEnable()
    {
        SubscribeButtons();
    }

    private void OnDisable()
    {
        UnsubscribeButtons();
    }
}