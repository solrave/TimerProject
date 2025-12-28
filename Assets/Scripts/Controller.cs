using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
   [SerializeField] private RectTransform _timerHolder1;
   [SerializeField] private RectTransform _timerHolder2;
   
   [SerializeField] private TimerView _simpleTimerView;
   [SerializeField] private TimerView _countdownTimerView;

   private Timer _simpleTimer;
   private Timer _countdownTimer;

   private void OnEnable()
   {
      _simpleTimer = new SimpleTimer(_simpleTimerView.GoalTime);
      _simpleTimerView.InjectTimer(_simpleTimer);
      SubscribeToView(_simpleTimerView);
   }

   private void SubscribeToView(TimerView view)
   {
      view.StopButtonPressed += StopButtonHandler;
   }

   private void StopButtonHandler(ITimerInstance timer)
   {
      
   }

   private void OnDisable()
   {
      throw new NotImplementedException();
   }

   private void RunTimer(Timer timer){}
   private void StopTimer(Timer timer){}
   private void ResumeTimer(Timer timer){}
   private void ResetTimer(Timer timer){}
}