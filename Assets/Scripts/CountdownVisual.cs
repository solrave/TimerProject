using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownVisual : VisualComponent
{
    [SerializeField] private RectTransform _timeProgressView;
    [SerializeField] private Image _imagePrefab;
    private Queue<Image> _images = new();

    private void OnDestroy()
    {
        _timer.CurrentSecondUpdated -= UpdateVisual;
        _timer.TimerStarted -= CreateImages;
    }

    public override void InitVisual(Timer timer)
    {
        base.InitVisual(timer);
        _timer.CurrentSecondUpdated += UpdateVisual;
        _timer.TimerStarted += CreateImages;
    }
    
    public override void ResetVisual()
    {
        ClearImages();
    }
    
    public override void UpdateVisual(float time)
    {
        
    }
    
    private void CreateImages()
    {
        ClearImages();
        for (int i = 0; i < _timer.GoalTime; i++)
        {
            var image = GameObject.Instantiate(_imagePrefab, _timeProgressView);
            _images.Enqueue(image);
        }
    }

    private void UpdateVisual(int time)
    {
        Destroy(_images.Dequeue().gameObject);
    }

    private void ClearImages()
    {
        foreach (Image image in _images)
        {
            Destroy(image.gameObject);
        }

        _images.Clear();
    }
}