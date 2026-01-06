using UnityEngine;

public abstract class VisualComponent : MonoBehaviour
{
    protected Timer _timer;

    public virtual void InitVisual(Timer timer)
    {
        _timer = timer;
    }
    public abstract void UpdateVisual(float time);
    public abstract void ResetVisual();
}