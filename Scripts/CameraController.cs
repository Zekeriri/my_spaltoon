using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 0.4f;
    public CinemachineVirtualCamera virtualCamera;
    public string mouseXInputName = "Mouse X";
    public string mouseYInputName = "Mouse Y";
    public float rotationSpeed = 0.1f;

    private CinemachineTransposer transposer;
    private bool adjustView = false;

    void Start()
    {
        if (virtualCamera != null)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }
    }

    void Update()
    {
        if (virtualCamera != null)
        {
            float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity;
            float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity;

            // 按下Y键开启视角调整
            if (Input.GetKeyDown(KeyCode.Y))
            {
                adjustView = !adjustView;
            }

            if (adjustView)
            {
                // 获取当前虚拟相机的跟随偏移值
                Vector3 followOffset = transposer.m_FollowOffset;

                // 根据鼠标移动修改偏移值
                followOffset.y -= mouseY * rotationSpeed;

                // 限制相机的Y轴旋转
                followOffset.y = Mathf.Clamp(followOffset.y, -10f, 10f);

                // 更新虚拟相机的跟随偏移值
                transposer.m_FollowOffset = followOffset;

                // 绕Y轴旋转摄像机
                transform.Rotate(Vector3.up, mouseX * rotationSpeed);
            }
            else
            {
                // 绕Y轴旋转摄像机
                transform.Rotate(Vector3.up, mouseX * rotationSpeed);
            }
        }
    }
}
