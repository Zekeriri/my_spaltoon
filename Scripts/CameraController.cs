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

            // ����Y�������ӽǵ���
            if (Input.GetKeyDown(KeyCode.Y))
            {
                adjustView = !adjustView;
            }

            if (adjustView)
            {
                // ��ȡ��ǰ��������ĸ���ƫ��ֵ
                Vector3 followOffset = transposer.m_FollowOffset;

                // ��������ƶ��޸�ƫ��ֵ
                followOffset.y -= mouseY * rotationSpeed;

                // ���������Y����ת
                followOffset.y = Mathf.Clamp(followOffset.y, -10f, 10f);

                // ������������ĸ���ƫ��ֵ
                transposer.m_FollowOffset = followOffset;

                // ��Y����ת�����
                transform.Rotate(Vector3.up, mouseX * rotationSpeed);
            }
            else
            {
                // ��Y����ת�����
                transform.Rotate(Vector3.up, mouseX * rotationSpeed);
            }
        }
    }
}
