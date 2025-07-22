using UnityEngine; // บรรทัดนี้จำเป็นสำหรับ Unity

public class PunchingBag : MonoBehaviour // ชื่อของไฟล์สคริปต์และชื่อคลาสต้องตรงกัน
{
    // ฟังก์ชันนี้จะถูกเรียกเมื่อมีวัตถุอื่น "ชน" เข้ากับกระสอบทราย
    // โดยที่ทั้งสองวัตถุต้องมี Collider และอย่างน้อยหนึ่งวัตถุมี Rigidbody
    [SerializeField] private PunchCounter punchCounter;
    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าวัตถุที่ชนเข้ามานั้นมี Tag ชื่อ "PlayerHand" หรือไม่
        // Tag "PlayerHand" คือสิ่งที่เราตั้งให้กับมือ (PlayerHand) ในขั้นตอนที่ 1
        if (other.gameObject.CompareTag("PlayerHand"))
        {
            // ถ้าวัตถุที่ชนมี Tag เป็น "PlayerHand"

            punchCounter.RegisterPunch();

            // แสดงข้อความใน Console ของ Unity เพื่อช่วยในการตรวจสอบ (Debug)
            Debug.Log("กระสอบทรายโดนต่อยแล้ว! คะแนนเพิ่ม!");
        }
    }


    // หมายเหตุ: ถ้าคุณต้องการให้การชนทำงานแบบ "Trigger" (คือชนแล้วทะลุไปเลย ไม่มีการกระเด้ง)
    // คุณจะต้องตั้งค่า Collider ของ PlayerHand ให้เป็น Is Trigger
    // และใช้ฟังก์ชัน OnTriggerEnter แทน OnCollisionEnter
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHand"))
        {
            ScoreManager.Instance.AddScore(1);
            Debug.Log("กระสอบทรายโดนต่อยแบบ Trigger!");
        }
    }
    */
}