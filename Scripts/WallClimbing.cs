using Splatoon;
using UnityEngine;
using UnityEngine.Rendering;

public class WallClimbing : MonoBehaviour
{
    public float wallCheckDistance = 0.8f;   // ���ǽ�ھ���
    public float climbSpeed = 8f;            // ��ǽ�ٶ�

    public bool isWallClimbing = false;     // �Ƿ�������ǽ
    public bool isFacingWall = false;       // �Ƿ�����ǽ��
    private CharacterController controller;  // ��ҿ�����
    public MoveScriptTest mst;
    public Transform groundCheck;           // �������


    public ParticleSystem wavewall;
    public GameObject splatoonSquidWall;
    public GameObject splatoonSquid;

    public SplatTestForDiving stfd;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        wavewall.Stop();
        splatoonSquidWall.SetActive(false);
    }

    private void Update()
    {
        if (splatoonSquid.activeSelf == true)
        {
            // ����Ƿ�����ǽ��
            isFacingWall = Physics.Raycast(groundCheck.position, transform.forward, wallCheckDistance);
        // ����������ж�
        if (isWallClimbing) // ����ǽ
        {
            mst.Velocity.y = 0;
            mst.gravity = 0f; // �ر�����
            wavewall.Play();
            splatoonSquid.SetActive(false);
            splatoonSquidWall.SetActive(true);
            // �ƶ����
            Vector3 climbDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetKey(KeyCode.W) ? 1f : 0f, 0f).normalized;
                controller.Move(climbDirection * climbSpeed * Time.deltaTime);

            if (!isFacingWall || Input.GetKeyDown(KeyCode.Space)) // ��������ǽ�ڻ��߰��¿ո��
            {
                isWallClimbing = false;
                mst.gravity = -15f; // ��������
                wavewall.Stop();
                splatoonSquidWall.SetActive(false);
            }
        }
        else // ������ǽ
        {
            mst.gravity = -15f; // ��������
            wavewall.Stop();
            splatoonSquidWall.SetActive(false);
            splatoonSquid.SetActive(true);
            if (isFacingWall) // ���ǽ��
            {
                if (Input.GetKeyDown(KeyCode.W)) // ��ʼ��ǽ
                {
                    isWallClimbing = true;
                }
                else if (Input.GetKey(KeyCode.W)) // ��ס W ��������Ƿ���ǽ�渽��
                {
                    RaycastHit hit2;
                    if (Physics.Raycast(groundCheck.position, transform.forward, out hit2, wallCheckDistance))
                    {
                        isWallClimbing = true;
                    }
                }
            }
        }
        }
        else 
        {
            mst.gravity = -15f;
            isWallClimbing = false;
            wavewall.Stop();
        }
    }
}
