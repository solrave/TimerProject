using System;
using Unity.VisualScripting;

public class Timer
{
    public event Action<float> PassedTime;
    public event Action<float> GoneTime;
    private float _elapsedTime;
    private float _passedTime = 0f;
    private bool _timerStarted;
    private bool _timerReset;//
    private bool _timerStopped;
    public Timer(float elapsedTime)
    {
        _elapsedTime = elapsedTime;
    }

    public void StartTimer(float deltaTime)
    {
        _passedTime += deltaTime;
        PassedTime?.Invoke(_passedTime);
        StartCountdownTimer(deltaTime);
    }
    public void ResetTimer(){}
    public void StopTimer(){}
    public void ResumeTimer(){}

    private void StartCountdownTimer(float deltaTime)
    {
        _elapsedTime -= (int)deltaTime;
        GoneTime?.Invoke(_elapsedTime);
    }
}