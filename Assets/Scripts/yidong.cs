using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yidong : MonoBehaviour
{
    
    public float moveSpeed = 5f;      // 移动速度
    public float mouseSensitivity = 2f; // 鼠标灵敏度
    public float verticalLookLimit = 80f; // 视角上下限制

    private float rotationX = 0f; // 水平旋转
    private CharacterController controller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 隐藏光标并锁定到屏幕中心
        controller = GetComponent<CharacterController>(); // 获取 CharacterController 组件
    }

    void Update()
    {
        // 鼠标控制视角
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // 角色移动
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * moveSpeed * Time.deltaTime);
    
    }
}