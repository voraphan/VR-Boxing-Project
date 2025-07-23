using UnityEngine; // บรรทัดนี้จำเป็นสำหรับ Unity
using TMPro;
using System;
using System.Collections.Generic;       // บรรทัดนี้สำคัญมาก! จำเป็นสำหรับการใช้ TextMeshPro

public class ScoreManager : MonoBehaviour // ชื่อของไฟล์สคริปต์และชื่อคลาสต้องตรงกัน
{
    // นี่คือ Singleton Pattern: ทำให้เราเข้าถึง ScoreManager นี้ได้จากที่ไหนก็ได้ในเกม
    // โดยใช้ ScoreManager.Instance
    public static ScoreManager Instance;
    public Action<int> OnScoreUpdate;
    public event Action<Difficulty, int> OnHighScoreUpdate;

    public int currentScore { get; private set; }

    // Awake() จะถูกเรียกครั้งแรกสุดเมื่อ GameObject ที่มีสคริปต์นี้ถูกสร้างขึ้นมา
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //ใส่ส่วนนี้ด้วย เพื่อใช้ในการข้ามฉาก
        DontDestroyOnLoad(gameObject);
    }

    // เพื่อเพิ่มคะแนน
    public void AddScore(Difficulty difficulty ,int amount)
    {
        currentScore += amount; // เพิ่มคะแนนตามจำนวนที่ส่งเข้ามา
        OnScoreUpdate?.Invoke(currentScore);      // เรียกฟังก์ชันเพื่ออัปเดตตัวเลขบนหน้าจอทันที
        UpdateHighScore(difficulty);
    }
    private void UpdateHighScore(Difficulty difficulty)
    {
        int savedScore = PlayerPrefs.GetInt(GetHighScoreKey(difficulty), 0);

        if (currentScore > savedScore)
        {
            PlayerPrefs.SetInt(GetHighScoreKey(difficulty), currentScore);
            PlayerPrefs.Save();

            OnHighScoreUpdate?.Invoke(difficulty, currentScore);
        }
    }


    public int GetHighScore(Difficulty difficulty)
    {
        return PlayerPrefs.GetInt(GetHighScoreKey(difficulty), 0);
    }

    private string GetHighScoreKey(Difficulty difficulty)
    {
        return $"HighScore_{difficulty}";
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
        OnScoreUpdate?.Invoke(currentScore);
    }

}