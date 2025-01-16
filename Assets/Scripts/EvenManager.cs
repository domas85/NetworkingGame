using UnityEngine;
using UnityEngine.Events;

public static class EvenManager
{
    public static event UnityAction TimerStart;
    public static event UnityAction TimerStop;
    public static event UnityAction<float> TimerUpdate;

    public static void OnTimerStart() => TimerStart?.Invoke();
    public static void OnTimerStop() => TimerStop?.Invoke();
    public static void OnTmerUpdate(float value) => TimerUpdate?.Invoke(value);
}
