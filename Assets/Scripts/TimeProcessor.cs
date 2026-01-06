using UnityEngine;

public abstract class TimeProcessor
{
    protected int _currentTime;
    protected readonly VisualComponent _component;

    protected TimeProcessor(VisualComponent component)
    {
        _component = component;
    }
    
    public abstract void ProcessTime(float time);
}

public class ForwardTimeProcessor : TimeProcessor
{
    public ForwardTimeProcessor(VisualComponent component) : base(component){ }

    public override void ProcessTime(float time)
    {
        if (Mathf.FloorToInt(time) % 1 == 0)
        {
            _component.UpdateVisual(time);
        }
    }
}

public class CountdownTimeProcessor : TimeProcessor
{
    public CountdownTimeProcessor(VisualComponent component) : base(component)
    {
    }

    public override void ProcessTime(float time)
    {
        
    }
}