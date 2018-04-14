using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class ObjectPoolTool : Singleton<ObjectPoolTool>
{
    [SerializeField] private bool lockPoolSize; //是否锁定长度限制
    [SerializeField] [Range(1, 20)] private int additionPoolSize; //无可用空间时,增长的数值
    [SerializeField] private List<ObjPool> goList;
    private Dictionary<GameObject, List<GameObject>> idleDic = new Dictionary<GameObject, List<GameObject>>();
    private Dictionary<GameObject, List<GameObject>> usingDic = new Dictionary<GameObject, List<GameObject>>();
    /// <summary>
    /// 初始化对象池
    /// </summary>
    public void InitPool()
    {
        foreach (ObjPool objPool in goList)
        {
            List<GameObject> temIdle = new List<GameObject>();
            List<GameObject> temUsing = new List<GameObject>();
            for (int i = 0; i < objPool.allocNum; i++)
            {
                GameObject item = GameObject.Instantiate(objPool.obj, Vector3.zero, Quaternion.identity);
                if (item == null)
                {
                    Debug.Log("初始化失败");
                }
                item.transform.parent = this.transform;
                item.gameObject.SetActive(false);
                temIdle.Add(item);
            }

            idleDic.Add(objPool.obj, temIdle);
            usingDic.Add(objPool.obj, temUsing);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        InitPool();

    }


    /// <summary>
    /// 使用对象池对象
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public GameObject Pop(GameObject go)
    {
        //休闲集合中有当前对象
        if (idleDic.ContainsKey(go))
        {
            List<GameObject> temIdle = new List<GameObject>(); //临时休闲对象
            idleDic.TryGetValue(go, out temIdle);
            List<GameObject> temUsing = new List<GameObject>(); //临时使用对象
            usingDic.TryGetValue(go, out temUsing);
            //当前链表中还有剩余可用的对象
            if (temIdle.Count > 0)
            {
                GameObject obj = temIdle[0];
                obj.SetActive(true);
                temIdle.Remove(obj);
                temUsing.Add(obj);
                return obj;
            }
            else
            {
//                Debug.Log("池子里没有对象了,正在构建");
                //如果不是锁定状态,增加数量
                if (!lockPoolSize)
                {
                    for (int i = 0; i < additionPoolSize; i++)
                    {
                        GameObject tempGo = GameObject.Instantiate(go, Vector3.zero, Quaternion.identity);
                        temIdle.Add(tempGo);
                    }

                    GameObject obj = temIdle[0];
                    obj.SetActive(true);
                    temIdle.Remove(obj);
                    temUsing.Add(obj);
                    return obj;
                }
            }
        }
        else
        {
            Debug.Log("没有构建过这个对象池");
        }

        return null;
    }

    /// <summary>
    /// 回收资源
    /// </summary>
    /// <param name="go"></param>
    public void Push(GameObject go)
    {
        go.SetActive(false);
        if (usingDic.ContainsKey(go))
        {
            List<GameObject> temIdle; //临时休闲对象
            idleDic.TryGetValue(go, out temIdle);
            List<GameObject> temUsing; //临时使用对象
            usingDic.TryGetValue(go, out temUsing);
            temIdle.Add(go);
            usingDic.Remove(go);
        }
    }
}

[Serializable]
public class ObjPool
{
    public GameObject obj; //对象池物体
    public int allocNum; //对象池大小
}