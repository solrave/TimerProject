using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForwardTimeVisual : VisualComponent
{

    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    public override void InitVisual(float time)
    {
        base.InitVisual(time);
        _slider.maxValue = _goalTime;
        _text.text = "0";
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