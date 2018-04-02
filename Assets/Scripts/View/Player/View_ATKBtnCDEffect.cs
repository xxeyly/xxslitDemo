using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 技能冷却时间
/// </summary>
public class View_ATKBtnCDEffect : MonoBehaviour
{
    public Image ImgCd; //CD图标
    public Text TextCD;
    public float FloCDTime = 3f; //冷却时间
    private float _FloTimerDelta = 0f;
    private bool IsStartTimer = true;
    public bool isSkillRead = false;
    void Start()
    {
        _FloTimerDelta = FloCDTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStartTimer)
        {
            _FloTimerDelta += Time.deltaTime;
            ImgCd.fillAmount = 1-(_FloTimerDelta / FloCDTime);
            TextCD.text = (FloCDTime - _FloTimerDelta).ToString();
            if (_FloTimerDelta >= FloCDTime)
            {
                isSkillRead = true;
                ImgCd.fillAmount = 0;
                TextCD.gameObject.SetActive(false);
                IsStartTimer = true;
            }
        }
    }

    public void ResponseBtnClick()
    {
        if (_FloTimerDelta < FloCDTime)
        {
//            Debug.Log("冷却时间未到...");
        }
        else
        {
            isSkillRead = false;
            _FloTimerDelta = 0;
            IsStartTimer = true;
            ImgCd.fillAmount = 1;
            TextCD.gameObject.SetActive(true);
            TextCD.text = FloCDTime.ToString();
        }
    }
}