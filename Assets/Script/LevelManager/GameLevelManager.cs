using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelManager : MonoBehaviour
{
    void Start()
    {
        Difficulty difficulty = StageProgressManager.Instance.GetCurrentStageLevel().difficulty;

        switch (difficulty)
        {
            case Difficulty.Easy:
                break;
            case Difficulty.Normal:
                break;
            case Difficulty.Hard:
                break;
        }
    }

    public void OnLevelCompleted()
    {
        //StageProgressManager.Instance.UnlockNextLevel();
    }
}
