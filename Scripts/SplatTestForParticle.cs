using Splatoon; // ����Splatoon�����ռ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��δ�����һ������ϵͳ�Ĳ��Խű���������������������ײʱ����ָ����ɫ�ʹ�С��Ϳ�ۡ�
namespace Splatoon
{
    public class SplatTestForParticle : MonoBehaviour // ���Ӳ��Խű�
    {
        public float splatScale = 0.1f; // Ϳ�۴�С
        //public Color splatColor = Color.red; // Ϳ����ɫ
        public Color splatColor = new Color(1f, 0.92f, 0.016f);


        private ParticleSystem particle; // ����ϵͳ
        private List<ParticleCollisionEvent> collisionEvents; // ��ײ�¼��б�

        public SplatTestForDiving stfd;

        // Start is called before the first frame update
        void Start()
        {
            particle = GetComponent<ParticleSystem>(); // ��ȡ����ϵͳ���
            particle.Stop(); // ֹͣ����ϵͳ
            collisionEvents = new List<ParticleCollisionEvent>(); // ������ײ�¼��б�
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0)&& stfd.r.enabled)
            {
                if (particle.isStopped)
                    particle.Play(); // �����������²�������ϵͳ����ֹͣ״̬����ʼ��������ϵͳ
            }
            else
            {
                if (particle.isPlaying)
                    particle.Stop(); // ������û�б����²�������ϵͳ���ڲ��ţ�ֹͣ��������ϵͳ
            }
        }

        private void OnParticleCollision(GameObject other) // ������ײ�¼�
        {
            int numCollisionEvents = particle.GetCollisionEvents(other, collisionEvents); // ��ȡ��ײ�¼�����

            SplatObject p = other.GetComponent<SplatObject>(); // ��ȡ��ײ�����Ϳ�۶������
            if (p != null)
            {
                for (int i = 0; i < numCollisionEvents; i++)
                {
                    SplatDataPool.Instance.AddCommand(collisionEvents[i].intersection, collisionEvents[i].normal, splatScale, splatColor); // ��Ϳ�����ݳ������Ϳ��ָ��
                }
            }
        }
    }
}
