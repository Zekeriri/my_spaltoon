using System.Collections;                  //����System.Collections�����ռ�
using System.Collections.Generic;          //����System.Collections.Generic�����ռ�
using UnityEngine;                           //����UnityEngine�����ռ�

//��δ��붨����һ����ΪSplatObject���࣬����̳���MonoBehaviour�ࡣ
//�ڸ����Awake�����У���ȡ�ö����Renderer��������Renderer�����Ϊ�գ�
//����SplatDataPool����ʵ����Ӹ�Renderer�����
//����������ڹ���Splatoon��Ϸ����Ҫ���Ƶ����塣

namespace Splatoon                            //���������ռ�Splatoon
{
    public class SplatObject : MonoBehaviour   //����SplatObject�࣬�̳���MonoBehaviour��
    {
        private void Awake()                    //Awake�����ڶ���ʵ����ʱ����
        {
            Renderer r = GetComponent<Renderer>();  //��ȡ�ö����Renderer���
            if (r != null)                       //���Renderer�����Ϊ��
            {
                SplatDataPool.Instance.AddRenderer(r);   //��SplatDataPool����ʵ�����Renderer���
            }
        }
    }
}
