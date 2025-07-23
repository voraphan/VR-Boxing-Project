using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScene : MonoBehaviour
{
    Difficulty difficulty;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;
    private int highScore;
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        if (!ScoreManager.Instance || !StageProgressManager.Instance) return;

        difficulty = StageProgressManager.Instance.GetCurrentStageLevel().difficulty;

        ScoreManager.Instance.OnScoreUpdate += OnScoreUpdated;
        ScoreManager.Instance.OnHighScoreUpdate += OnHighScoreUpdated;

        currentScore = ScoreManager.Instance.currentScore;
        highScore = ScoreManager.Instance.GetHighScore(difficulty);

        level = StageProgressManager.Instance.GetCurrentStageLevel().stageIndex;

        UpdateText();
    }


    private void OnScoreUpdated(int newScore)
    {
        currentScore = newScore;
        UpdateText();
    }

    private void OnHighScoreUpdated(Difficulty updatedDifficulty, int newHighScore)
    {
        if (updatedDifficulty != difficulty) return;

        highScore = newHighScore;
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = $" {difficulty} Level {level+1}\nScore: {currentScore}\nHigh Score: {highScore}";
    }
    private void OnDestroy()
    {
         ScoreManager.Instance.OnScoreUpdate -= OnScoreUpdated;
         ScoreManager.Instance.OnHighScoreUpdate -= OnHighScoreUpdated;
    }
}
