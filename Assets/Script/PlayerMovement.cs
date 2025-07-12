using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f; // ความเร็วการหมุน (องศา/วินาที)

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // เดินหน้า-ถอยหลัง
        float vertical = Input.GetAxis("Vertical"); // W/S หรือ ↑/↓
        Vector3 move = transform.forward * vertical;

        // หมุนซ้าย-ขวา
        float horizontal = Input.GetAxis("Horizontal"); // A/D หรือ ←/→
        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);

        // เคลื่อนที่
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
