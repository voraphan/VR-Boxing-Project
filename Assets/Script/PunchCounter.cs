using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchCounter : MonoBehaviour
{
    public int targetPunches = 10;
    private int currentPunches = 0;
    public GameObject coachDialogBox; // ช่องแสดงคำพูดโค้ช
    public string nextSceneName = "EndScene"; // ตั้งชื่อฉากต่อไป

    public void RegisterPunch()
    {
        currentPunches++;

        if (currentPunches >= targetPunches)
        {
            EndTraining();
        }
    }

    void EndTraining()
    {
        // แสดงคำพูดโค้ช
        if (coachDialogBox != null)
        {
            coachDialogBox.SetActive(true);
        }

        // หรือโหลดฉากใหม่ (หลังจากดีเลย์เล็กน้อย)
        Invoke("LoadNextScene", 2f);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}