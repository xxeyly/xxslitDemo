using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_HubText : MonoBehaviour
{
    private bool isShow = false;
    [SerializeField]private float speed = 100;

    void Update()
    {
        if (isShow)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }

    /// <summary>
    /// 血条的显示
    /// </summary>
    public void ShowHud()
    {
        isShow = true;
        StartCoroutine(AutoHideHub());
    }

    IEnumerator AutoHideHub()
    {
        yield return new WaitForSeconds(1f);
        isShow = false;
        ObjectPoolTool.Instance.Push(this.gameObject);
    }
}