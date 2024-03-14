using Splatoon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatTestForWall : MonoBehaviour
{
    public RayCheck downCheck;
    public RayCheck forwardCheck; // 新添加的 RayCheck 对象
    public Color splatColor;
    public ParticleSystem wave;
    public Renderer r;
    public MoveScriptTest mst;
    public bool canSwim = false; // 新添加的变量

    void Start()
    {
        r = GetComponent<Renderer>();
        wave.Stop();
    }

    void Update()
    {
        // 减速
        if (mst.IsGrounded || r.enabled)
        {
            mst.speed -= mst.acceleration * Time.deltaTime;
            mst.speed = Mathf.Max(mst.speed, 5f);
        }

        // 检测前方是否有可潜水的墙
        RaycastHit hitForward;
        bool hitWall = forwardCheck.Shoot(transform.position, transform.forward, out hitForward);
        bool hitColor = hitWall && IsSameColor(splatColor, hitForward.collider.GetComponent<Renderer>().material.color);

        // 如果击中了可潜水的墙，标记为可以潜水
        canSwim = hitColor;

        // 如果可以潜水并按下 F 键，关闭角色 MeshRenderer 组件并播放波浪特效
        if (canSwim && Input.GetKeyDown(KeyCode.F))
        {
            r.enabled = false;
            if (wave.isStopped)
                wave.Play();
            mst.speed = 10;
        }
        // 如果不可以潜水或者没有按下 F 键，开启角色 MeshRenderer 组件并停止播放波浪特效
        else
        {
            r.enabled = true;
            if (wave.isPlaying)
                wave.Stop();
        }
    }

    public bool IsSameColor(Color c1, Color c2)
    {
        return Mathf.Abs(c1.r - c2.r) + Mathf.Abs(c1.g - c2.g) + Mathf.Abs(c1.b - c2.b) < 0.1f;
    }

    public void OnDrawGizmos()
    {
        downCheck.DrawGizmos(transform.position, 0.1f);
        forwardCheck.DrawGizmos(transform.position, 0.1f);
    }
}
