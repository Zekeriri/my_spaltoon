using Splatoon; // 引入Splatoon命名空间
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这段代码是一个粒子系统的测试脚本，用于在与其他物体碰撞时生成指定颜色和大小的涂痕。
namespace Splatoon
{
    public class SplatTestForParticle : MonoBehaviour // 粒子测试脚本
    {
        public float splatScale = 0.1f; // 涂痕大小
        //public Color splatColor = Color.red; // 涂痕颜色
        public Color splatColor = new Color(1f, 0.92f, 0.016f);


        private ParticleSystem particle; // 粒子系统
        private List<ParticleCollisionEvent> collisionEvents; // 碰撞事件列表

        public SplatTestForDiving stfd;

        // Start is called before the first frame update
        void Start()
        {
            particle = GetComponent<ParticleSystem>(); // 获取粒子系统组件
            particle.Stop(); // 停止粒子系统
            collisionEvents = new List<ParticleCollisionEvent>(); // 创建碰撞事件列表
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0)&& stfd.r.enabled)
            {
                if (particle.isStopped)
                    particle.Play(); // 如果左键被按下并且粒子系统处于停止状态，开始播放粒子系统
            }
            else
            {
                if (particle.isPlaying)
                    particle.Stop(); // 如果左键没有被按下并且粒子系统正在播放，停止播放粒子系统
            }
        }

        private void OnParticleCollision(GameObject other) // 粒子碰撞事件
        {
            int numCollisionEvents = particle.GetCollisionEvents(other, collisionEvents); // 获取碰撞事件数量

            SplatObject p = other.GetComponent<SplatObject>(); // 获取碰撞对象的涂痕对象组件
            if (p != null)
            {
                for (int i = 0; i < numCollisionEvents; i++)
                {
                    SplatDataPool.Instance.AddCommand(collisionEvents[i].intersection, collisionEvents[i].normal, splatScale, splatColor); // 在涂痕数据池中添加涂痕指令
                }
            }
        }
    }
}
