using Splatoon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatTestForWall : MonoBehaviour
{
    public RayCheck downCheck;
    public RayCheck forwardCheck; // ����ӵ� RayCheck ����
    public Color splatColor;
    public ParticleSystem wave;
    public Renderer r;
    public MoveScriptTest mst;
    public bool canSwim = false; // ����ӵı���

    void Start()
    {
        r = GetComponent<Renderer>();
        wave.Stop();
    }

    void Update()
    {
        // ����
        if (mst.IsGrounded || r.enabled)
        {
            mst.speed -= mst.acceleration * Time.deltaTime;
            mst.speed = Mathf.Max(mst.speed, 5f);
        }

        // ���ǰ���Ƿ��п�Ǳˮ��ǽ
        RaycastHit hitForward;
        bool hitWall = forwardCheck.Shoot(transform.position, transform.forward, out hitForward);
        bool hitColor = hitWall && IsSameColor(splatColor, hitForward.collider.GetComponent<Renderer>().material.color);

        // ��������˿�Ǳˮ��ǽ�����Ϊ����Ǳˮ
        canSwim = hitColor;

        // �������Ǳˮ������ F �����رս�ɫ MeshRenderer ��������Ų�����Ч
        if (canSwim && Input.GetKeyDown(KeyCode.F))
        {
            r.enabled = false;
            if (wave.isStopped)
                wave.Play();
            mst.speed = 10;
        }
        // ���������Ǳˮ����û�а��� F ����������ɫ MeshRenderer �����ֹͣ���Ų�����Ч
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
