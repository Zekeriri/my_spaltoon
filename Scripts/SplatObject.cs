using System.Collections;                  //导入System.Collections命名空间
using System.Collections.Generic;          //导入System.Collections.Generic命名空间
using UnityEngine;                           //导入UnityEngine命名空间

//这段代码定义了一个名为SplatObject的类，该类继承自MonoBehaviour类。
//在该类的Awake方法中，获取该对象的Renderer组件，如果Renderer组件不为空，
//则向SplatDataPool单例实例添加该Renderer组件。
//该类可能用于管理Splatoon游戏中需要绘制的物体。

namespace Splatoon                            //定义命名空间Splatoon
{
    public class SplatObject : MonoBehaviour   //定义SplatObject类，继承自MonoBehaviour类
    {
        private void Awake()                    //Awake方法在对象被实例化时调用
        {
            Renderer r = GetComponent<Renderer>();  //获取该对象的Renderer组件
            if (r != null)                       //如果Renderer组件不为空
            {
                SplatDataPool.Instance.AddRenderer(r);   //向SplatDataPool单例实例添加Renderer组件
            }
        }
    }
}
