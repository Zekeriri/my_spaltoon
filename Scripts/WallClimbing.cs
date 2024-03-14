using Splatoon;
using UnityEngine;
using UnityEngine.Rendering;

public class WallClimbing : MonoBehaviour
{
    public float wallCheckDistance = 0.8f;   // 检测墙壁距离
    public float climbSpeed = 8f;            // 爬墙速度

    public bool isWallClimbing = false;     // 是否正在爬墙
    public bool isFacingWall = false;       // 是否面向墙壁
    private CharacterController controller;  // 玩家控制器
    public MoveScriptTest mst;
    public Transform groundCheck;           // 地面检测点


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
            // 检测是否面向墙壁
            isFacingWall = Physics.Raycast(groundCheck.position, transform.forward, wallCheckDistance);
        // 处理输入和行动
        if (isWallClimbing) // 在爬墙
        {
            mst.Velocity.y = 0;
            mst.gravity = 0f; // 关闭重力
            wavewall.Play();
            splatoonSquid.SetActive(false);
            splatoonSquidWall.SetActive(true);
            // 移动玩家
            Vector3 climbDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetKey(KeyCode.W) ? 1f : 0f, 0f).normalized;
                controller.Move(climbDirection * climbSpeed * Time.deltaTime);

            if (!isFacingWall || Input.GetKeyDown(KeyCode.Space)) // 不再面向墙壁或者按下空格键
            {
                isWallClimbing = false;
                mst.gravity = -15f; // 开启重力
                wavewall.Stop();
                splatoonSquidWall.SetActive(false);
            }
        }
        else // 不在爬墙
        {
            mst.gravity = -15f; // 开启重力
            wavewall.Stop();
            splatoonSquidWall.SetActive(false);
            splatoonSquid.SetActive(true);
            if (isFacingWall) // 面对墙壁
            {
                if (Input.GetKeyDown(KeyCode.W)) // 开始爬墙
                {
                    isWallClimbing = true;
                }
                else if (Input.GetKey(KeyCode.W)) // 按住 W 键，检测是否在墙面附近
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
