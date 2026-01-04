using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimerView : TimeView
{
    [SerializeField] private RectTransform _timeProgressView;
    [SerializeField] private Image _imagePrefab;
    private Queue<Image> _images = new();

    protected override void RunTimeCounter()
    {
        if (_routine != null) return;
        InstantiateImages();
        base.RunTimeCounter();
    }
    
    protected override void OnTimeChanged(float time)
    {
       Destroy(_images.Dequeue().gameObject);
    }
    
    protected override void ResetThisView()
    {
        ClearImages();
        Debug.Log($"Timer finished at {_timeCounter.CurrentTime} !");
    }

    private void InstantiateImages()
    {
        for (int i = 0; i < _goalTime; i++)
        {
            var image = Instantiate(_imagePrefab, _timeProgressView);
            _images.Enqueue(image);
        }
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