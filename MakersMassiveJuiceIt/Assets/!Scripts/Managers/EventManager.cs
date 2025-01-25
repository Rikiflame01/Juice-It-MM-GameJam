using UnityEngine;
using System;

public static class EventManager
{
    public static Action OnScreenShake;

    public static Action<int> OnIncreaseScore;

    public static void TriggerScreenShake()
    {
        OnScreenShake?.Invoke();
    }

    public static void TriggerIncreaseScore(int score)
    {
        OnIncreaseScore?.Invoke(score);
    }

}