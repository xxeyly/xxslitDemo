using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

public class Ctrl_GameReading : Singleton<Ctrl_GameReading>
{
    private string m_sFileName; // 文件名
    private string m_sPath; // 路径
    private ArrayList m_aArray; // 文本中每行的内容
    private List<TextAsset> cars = new List<TextAsset>();

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 保存进度
    /// </summary>
    public void SaveGame()
    {
        m_sPath = Application.dataPath + "/Resources/" + "/Data";
        getCarTextName();
        if (cars.Count >= 1)
        {
            m_sFileName = "FileName" + (cars.Count + 1) + ".Json";
        }
        else
        {
            m_sFileName = "FileName.Json";
        }

        string gameData = JsonMapper.ToJson(Ctrl_HeroProperty.Instance.GetHeroCurrentData());
//        Debug.Log(gameData);
        fnCreateFile(m_sPath, m_sFileName, gameData);
    }

    //读取当前存档目录有多少个存档
    public void getCarTextName()
    {
        if (Directory.Exists(m_sPath))
        {
            //获取文件信息
            DirectoryInfo direction = new DirectoryInfo(m_sPath);
            //所有的文件名字
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

//            print(files.Length);

            for (int i = 0; i < files.Length; i++)
            {
                //过滤掉临时文件
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }

//                print(files[i].Extension); //这个是扩展名
                //获取不带扩展名的文件名
                string name = Path.GetFileNameWithoutExtension(files[i].ToString());
                print(name);
                // FileInfo.Name是返回带扩展名的名字 
                cars.Add(Resources.Load<TextAsset>("Data/"+name));
            }
        }

        foreach (TextAsset car in cars)
        {
            Debug.Log(car.name);
        }
    }

    /// <summary>
    /// 将字符串转换成文本数据,存储
    /// </summary>
    /// <param name="sPath">文件路径</param>
    /// <param name="sName">文件名称</param>
    /// <param name="nDate">文件数据</param>
    void fnCreateFile(string sPath, string sName, string nDate)
    {
        StreamWriter t_sStreamWriter; // 文件流信息
        FileInfo t_fFileInfo = new FileInfo(sPath + "//" + sName);
        if (!t_fFileInfo.Exists)
        {
            t_sStreamWriter = t_fFileInfo.CreateText(); // 如果此文件不存在则创建
        }
        else
        {
            t_sStreamWriter = t_fFileInfo.AppendText(); // 如果此文件存在则打开
        }

        t_sStreamWriter.WriteLine(nDate); // 以行的形式写入信息 
        t_sStreamWriter.Close(); //关闭流
        t_sStreamWriter.Dispose(); // 销毁流
    }
}