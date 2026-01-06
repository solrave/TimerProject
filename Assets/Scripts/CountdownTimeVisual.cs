using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimeVisual : VisualComponent
{
    [SerializeField] private RectTransform _timeProgressView;
    [SerializeField] private Image _imagePrefab;
    private Queue<Image> _images = new();
    
    public override void InitVisual(float time)
    {
        base.InitVisual(time);
    }

    public override void UpdateVisual(float time)
    {
        
    }

    public override void ResetVisual()
    {
        
    }
}