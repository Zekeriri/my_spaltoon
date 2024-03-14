using UnityEngine;

public class MoveScriptTest : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f; // 移动速度
    public float acceleration = 10f; // 加速度
    public float gravity = -15f; // 重力加速度
    public Vector3 Velocity = Vector3.zero;
    public Transform GroundCheck;
    public float GroundRadius = 0.1f;
    public bool IsGrounded;
    public LayerMask GroundlayerMask;
    public float mouseSensitivity = 100f; // 鼠标灵敏度
    public float JumpHight = 3f;

    // Add a public Transform variable for the camera
    public Transform cameraTransform;
    private float verticalLookRotation; // Store the camera's vertical rotation

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // 隐藏并锁定鼠标指针，让玩家能够专注于游戏
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update 每帧调用一次
    void Update()
    {
        MoveLikeWow();
    }

    private void MoveLikeWow()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundRadius, GroundlayerMask);
        if (IsGrounded && Velocity.y < 0)
        {
            Velocity.y = 0;
        }

        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            Velocity.y += Mathf.Sqrt(JumpHight * -2f * gravity);
        }

        // 获取输入
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 计算角色旋转
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.y += mouseX;
        transform.localRotation = Quaternion.Euler(rotation);

        // 计算相机旋转
        // Use the verticalLookRotation variable to store the camera's vertical rotation
        verticalLookRotation += mouseY * mouseSensitivity * Time.deltaTime;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraTransform.localEulerAngles = new Vector3(-verticalLookRotation, 0f, 0f);

        // 计算移动方向
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        // 移动
        controller.Move(move * speed * Time.deltaTime);
        // 重力
        Velocity.y += gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);
    }
}
