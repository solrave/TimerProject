using System;
using Unity.VisualScripting;

public class Timer
{
    public event Action<float> PassedTime;
    public event Action<int> GoneTime;
    private int _elapsedTime;
    private float _passedTime = 0f;
    public Timer(int elapsedTime)
    {
        _elapsedTime = elapsedTime;
    }

    public void StartTimer(float deltaTime)
    {
        _passedTime += deltaTime;
        PassedTime?.Invoke(_passedTime);
    }
    public void ResetTimer(){}
    public void StopTimer(){}
    public void ResumeTimer(){}

    private void StartCountdownTimer()
    {
    }
}