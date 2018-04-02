using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_NotifcationLevel : MonoBehaviour
{
    [SerializeField] private Text headText;
    [SerializeField] private Text contentText;

    public void ShowNotification(string contentStr, float showTime, string headStr)
    {
        headText.text = headStr;
        contentText.text = contentStr;
        GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(HideNotification(showTime));
    }

    IEnumerator HideNotification(float showTime)
    {
        yield return new WaitForSeconds(showTime);
        GetComponent<CanvasGroup>().alpha = 0;
    }
}