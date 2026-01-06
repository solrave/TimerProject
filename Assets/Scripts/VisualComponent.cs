using UnityEngine;

public abstract class VisualComponent : MonoBehaviour
{
    protected float _goalTime;

    public virtual void InitVisual(float time)
    {
        _goalTime = time;
    }
    public abstract void UpdateVisual(float time);
    public abstract void ResetVisual();
}