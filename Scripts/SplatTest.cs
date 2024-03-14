using Splatoon;                            //导入Splatoon命名空间
using System.Collections;                  //导入System.Collections命名空间
using System.Collections.Generic;          //导入System.Collections.Generic命名空间
using UnityEngine;                           //导入UnityEngine命名空间
//这段代码定义了一个名为SplatTest的类，该类继承自MonoBehaviour类。
//该类包含了maxDistance、checkLayer、splatScale、splatColor等变量。
//在Update方法中，如果鼠标左键被按下，会从相机发射一条射线，
//如果射线与物体相交，则向SplatDataPool单例实例添加指令
namespace Splatoon                            //定义命名空间Splatoon
{
    public class SplatTest : MonoBehaviour     //定义SplatTest类，继承自MonoBehaviour类
    {
        public float maxDistance = 100;         //定义float类型变量maxDistance并赋值为100
        public LayerMask checkLayer;            //定义LayerMask类型变量checkLayer
        public float splatScale = 0.1f;         //定义float类型变量splatScale并赋值为0.1
        public Color splatColor = Color.red;    //定义Color类型变量splatColor并赋值为红色

        // Update is called once per frame      //Update方法每帧调用一次
        void Update()
        {
            if (Input.GetMouseButton(0))        //如果鼠标左键被按下
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //获取从相机发射的射线
                RaycastHit hit;                //定义RaycastHit类型变量hit
                if (Physics.Raycast(ray, out hit, maxDistance, checkLayer))     //如果相机射线与物体相交
                {
                    SplatDataPool.Instance.AddCommand(hit.point, hit.normal, splatScale, splatColor);  //向SplatDataPool单例实例添加指令
                }
            }
        }
    }
}
