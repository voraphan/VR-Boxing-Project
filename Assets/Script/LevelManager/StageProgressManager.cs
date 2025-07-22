using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

[System.Serializable]
public struct StageLevel
{
    public int stageIndex;       // 0-based: Stage 1 = 0
    public Difficulty difficulty;
}

public class StageProgressManager : MonoBehaviour
{
    public static StageProgressManager Instance;

    private StageLevel currentLevel;
    private Dictionary<Difficulty, int> progressIndex = new Dictionary<Difficulty, int>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Difficulty diff in System.Enum.GetValues(typeof(Difficulty)))
        {
            progressIndex[diff] = 0;
        }
    }

    public void UnlockNextStage()
    {
        int currentIndex = currentLevel.stageIndex;
        Difficulty difficulty = currentLevel.difficulty;

        if (currentIndex < 2) // 0,1,2
        {
            int savedProgress = progressIndex[difficulty];
            if (currentIndex + 1 > savedProgress)
            {
                progressIndex[difficulty] = currentIndex + 1;
            }
        }
    }

    public bool IsStageUnlocked(Difficulty difficulty, int stageIndex)
    {
        return progressIndex.TryGetValue(difficulty, out int unlockedIndex) && stageIndex <= unlockedIndex;
    }

    public void SetCurrentStage(Difficulty difficulty, int stageIndex)
    {
        currentLevel.difficulty = difficulty;
        currentLevel.stageIndex = stageIndex;
    }

    public StageLevel GetCurrentStageLevel()
    {
        return currentLevel;
    }

    public int GetProgressIndex(Difficulty difficulty)
    {
        return progressIndex.TryGetValue(difficulty, out int idx) ? idx : 0;
    }
}