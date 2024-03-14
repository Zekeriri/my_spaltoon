using Splatoon;                            //����Splatoon�����ռ�
using System.Collections;                  //����System.Collections�����ռ�
using System.Collections.Generic;          //����System.Collections.Generic�����ռ�
using UnityEngine;                           //����UnityEngine�����ռ�
//��δ��붨����һ����ΪSplatTest���࣬����̳���MonoBehaviour�ࡣ
//���������maxDistance��checkLayer��splatScale��splatColor�ȱ�����
//��Update�����У���������������£�����������һ�����ߣ�
//��������������ཻ������SplatDataPool����ʵ�����ָ��
namespace Splatoon                            //���������ռ�Splatoon
{
    public class SplatTest : MonoBehaviour     //����SplatTest�࣬�̳���MonoBehaviour��
    {
        public float maxDistance = 100;         //����float���ͱ���maxDistance����ֵΪ100
        public LayerMask checkLayer;            //����LayerMask���ͱ���checkLayer
        public float splatScale = 0.1f;         //����float���ͱ���splatScale����ֵΪ0.1
        public Color splatColor = Color.red;    //����Color���ͱ���splatColor����ֵΪ��ɫ

        // Update is called once per frame      //Update����ÿ֡����һ��
        void Update()
        {
            if (Input.GetMouseButton(0))        //���������������
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //��ȡ��������������
                RaycastHit hit;                //����RaycastHit���ͱ���hit
                if (Physics.Raycast(ray, out hit, maxDistance, checkLayer))     //�����������������ཻ
                {
                    SplatDataPool.Instance.AddCommand(hit.point, hit.normal, splatScale, splatColor);  //��SplatDataPool����ʵ�����ָ��
                }
            }
        }
    }
}
