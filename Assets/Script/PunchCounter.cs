using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchCounter : MonoBehaviour
{
    private bool isTrainingEnded = false;
    private int targetPunches;
    private int currentPunches = 0;
    public GameObject coachDialogBox; // ช่องแสดงคำพูดโค้ช

    private Dictionary<Difficulty, int[]> targetPunchTable = new Dictionary<Difficulty, int[]>
{
    { Difficulty.Easy,   new int[] { 5, 10, 15 } },
    { Difficulty.Normal, new int[] { 6, 9, 12 } },
    { Difficulty.Hard,   new int[] { 8, 16, 24 } }
};

    private void Start()
    {
        isTrainingEnded = false ;
        StageLevel currentLevel = new StageLevel { stageIndex = 0, difficulty = Difficulty.Easy };
        if (StageProgressManager.Instance != null)
             currentLevel = StageProgressManager.Instance.GetCurrentStageLevel();

        if (targetPunchTable.TryGetValue(currentLevel.difficulty, out int[] punches))
        {
            if (currentLevel.stageIndex >= 0 && currentLevel.stageIndex < punches.Length)
            {
                targetPunches = punches[currentLevel.stageIndex];
            }
            else
            {
                Debug.LogWarning("Stage index เกินจำนวนที่กำหนดไว้");
            }
        }
        else
        {
            Debug.LogWarning("Difficulty นี้ไม่มีในตาราง targetPunches");
        }
    }

    public void RegisterPunch()
    {
        if (isTrainingEnded)
            return;

        currentPunches++;
        Difficulty difficulty = Difficulty.Easy;
        if (StageProgressManager.Instance)
            difficulty = StageProgressManager.Instance.GetCurrentStageLevel().difficulty;
        if (ScoreManager.Instance)
            ScoreManager.Instance.AddScore(difficulty, 1); // เพิ่มคะแนน 1 แต้มทุกครั้งที่ต่อยโดน

        if (currentPunches >= targetPunches)
        {
            EndTraining();
        }
    }

    void EndTraining()
    {
        isTrainingEnded = true;
        // แสดงคำพูดโค้ช
        if (coachDialogBox != null)
        {
            coachDialogBox.SetActive(true);
        }

        // หรือโหลดฉากใหม่ (หลังจากดีเลย์เล็กน้อย)
        StartCoroutine(WaitAndLoadNextScene());
    }

    private IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(2f); // รอดีเลย์ 2 วินาที

        // ปลดล็อกด่านถัดไป
        StageProgressManager.Instance.UnlockNextStage();

        // ดึงค่าด่านปัจจุบัน
        StageLevel currentLevel = StageProgressManager.Instance.GetCurrentStageLevel();

        // หาชื่อ scene ถัดไป (ตามระบบคุณอาจแมปชื่อไว้ หรือมี scene ชื่อ Stage_Easy_0, Stage_Easy_1, ...)
        int nextStageIndex = currentLevel.stageIndex + 1;
        Difficulty difficulty = currentLevel.difficulty;

        if (nextStageIndex > 2)
        {
            Debug.Log("เล่นจบด่านสุดท้ายแล้ว");
            SceneManager.LoadScene("VRMenu");
            ScoreManager.Instance.ResetCurrentScore();
            yield break;
        }

        // ตั้งค่าไปด่านถัดไป
        StageProgressManager.Instance.SetCurrentStage(difficulty, nextStageIndex);

        string nextSceneName = $"Stage_{difficulty}_{nextStageIndex}";
        Debug.Log($"โหลดฉากถัดไป: {nextSceneName}");

        string sceneToLoad = $"{difficulty}";
        SceneManager.LoadScene(sceneToLoad);
    }
}