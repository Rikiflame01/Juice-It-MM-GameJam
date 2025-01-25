using UnityEngine;
using System;

public static class EventManager
{
    public static Action OnScreenShake;

    public static Action<float> OnIncreaseScore;

    public static void TriggerScreenShake()
    {
        OnScreenShake?.Invoke();
    }

    public static void TriggerIncreaseScore(float score)
    {
        OnIncreaseScore?.Invoke(score);
    }

}