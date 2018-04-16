using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class Ctrl_PlayerAccessFile : Singleton<Ctrl_PlayerAccessFile>
{
    [SerializeField] private Transform content; //存取档放置区
    private Model_Archiving currentArchiving; //当前存档
    [SerializeField] private bool isArchiving; //当前是存档还是读档
    [SerializeField] private Text titleContent;

    public void UpdateContent()
    {
        if (Ctrl_PlayerAccessFile.Instance.IsArchiving)
        {
            titleContent.text = "存档";
        }
        else
        {
            titleContent.text = "读档";
        }
    }

    public bool IsArchiving
    {
        get { return isArchiving; }

        set
        {
            isArchiving = value;
            UpdateContent();
        }
    }

    void Start()
    {
        Ctrl_GameReading.Instance.GetCarTextName();
        StartCoroutine(GetArchivingList());
    }

    /// <summary>
    /// 先获得存档
    /// </summary>
    IEnumerator GetArchivingList()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 20; i++)
        {
            GameObject archivingItem = ObjectPoolTool.Instance.Pop(ObjectPoolTool.Instance.GetObject(objPool.ArchivingItem));
            archivingItem.SetActive(true);
            archivingItem.transform.parent = content;
            archivingItem.transform.localScale = Vector3.one;
            archivingItem.GetComponent<Ctrl_ArchivingItem>().Index = i;
            if (i < Ctrl_GameReading.Instance.Cars.Count)
            {
                archivingItem.GetComponent<Ctrl_ArchivingItem>().Archiving =
                    JsonMapper.ToObject<Model_Archiving>(Ctrl_GameReading.Instance.Cars[i].ToString());

            }
        }
    }
}