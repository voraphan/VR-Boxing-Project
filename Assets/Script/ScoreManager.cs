using UnityEngine; // บรรทัดนี้จำเป็นสำหรับ Unity
using TMPro;       // บรรทัดนี้สำคัญมาก! จำเป็นสำหรับการใช้ TextMeshPro

public class ScoreManager : MonoBehaviour // ชื่อของไฟล์สคริปต์และชื่อคลาสต้องตรงกัน
{
    // นี่คือ Singleton Pattern: ทำให้เราเข้าถึง ScoreManager นี้ได้จากที่ไหนก็ได้ในเกม
    // โดยใช้ ScoreManager.Instance
    public static ScoreManager Instance;

    // ตัวแปรนี้ใช้เก็บการอ้างอิงถึง UI TextMeshPro ที่เราสร้างไว้ในขั้นตอนที่ 1
    // เราจะต้องลาก UI Text นั้นมาใส่ในช่องนี้ใน Inspector ของ Unity
    public TextMeshProUGUI scoreText;

    // ตัวแปรนี้จะเก็บคะแนนปัจจุบันของเรา
    private int currentScore = 0;

    // Awake() จะถูกเรียกครั้งแรกสุดเมื่อ GameObject ที่มีสคริปต์นี้ถูกสร้างขึ้นมา
    void Awake()
    {
        // ตรวจสอบว่ามี ScoreManager อื่นอยู่แล้วในฉากหรือไม่
        if (Instance == null)
        {
            // ถ้ายังไม่มี ก็กำหนดให้ Instance นี้เป็นตัว ScoreManager หลัก
            Instance = this;
        }
        else
        {
            // ถ้ามี ScoreManager อื่นอยู่แล้ว ก็ทำลายตัวเองทิ้งไป (เพื่อไม่ให้มี ScoreManager หลายตัว)
            Destroy(gameObject);
        }
    }

    // Start() จะถูกเรียกหนึ่งครั้งเมื่อเกมเริ่มทำงาน (หลังจาก Awake())
    void Start()
    {
        // เมื่อเกมเริ่ม ให้แสดงคะแนนเริ่มต้น (ซึ่งคือ 0) บน UI ทันที
        UpdateScoreText();
    }

    // ฟังก์ชันนี้เป็นแบบ Public ทำให้สคริปต์อื่นๆ (เช่น PunchingBag) สามารถเรียกใช้ได้
    // เพื่อเพิ่มคะแนน
    public void AddScore(int amount)
    {
        currentScore += amount; // เพิ่มคะแนนตามจำนวนที่ส่งเข้ามา
        UpdateScoreText();      // เรียกฟังก์ชันเพื่ออัปเดตตัวเลขบนหน้าจอทันที
    }

    // ฟังก์ชันนี้จะทำหน้าที่อัปเดตข้อความบน UI Text ให้แสดงคะแนนปัจจุบัน
    void UpdateScoreText()
    {
        // ตรวจสอบว่าเราได้ลาก UI Text มาใส่ในช่อง scoreText แล้วหรือยัง
        if (scoreText != null)
        {
            // กำหนดข้อความของ UI Text ให้เป็น "Score: " ตามด้วยคะแนนปัจจุบัน
            scoreText.text = "Score: " + currentScore.ToString();
        }
        else
        {
            // ถ้า scoreText ยังเป็นค่าว่าง (null) ให้แสดง Error ใน Console
            Debug.LogError("Score Text (TextMeshProUGUI) ไม่ได้ถูกเชื่อมต่อใน ScoreManager!");
        }
    }
}