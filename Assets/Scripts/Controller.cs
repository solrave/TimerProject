using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
   [SerializeField] private RectTransform _timerHolder1;
   [SerializeField] private RectTransform _timerHolder2;
   
   [SerializeField] private TimeView _stopWatchView;
   [SerializeField] private TimeView _timerView;

   private TimeCounter _stopWatch;
   private TimeCounter _timer;

   private void OnEnable()
   {
      _stopWatch = new Stopwatch(_stopWatchView); 
      _stopWatchView.InjectTimeCounter(_stopWatch);

      _timer = new Timer(_timerView);
      _timerView.InjectTimeCounter(_timer);
   }

   private void OnDisable()
   {
      
   }

   private void RunTimer(TimeCounter timeCounter){}
   private void StopTimer(TimeCounter timeCounter){}
   private void ResumeTimer(TimeCounter timeCounter){}
   private void ResetTimer(TimeCounter timeCounter){}
}