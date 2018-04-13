using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class View_LoadingScenes : Singleton<View_LoadingScenes>
{
    public Slider SliLoadingProgress; //进度条控件
    private float _FloProgressNumber;
    [SerializeField] private string sceneName;

    private void Start()
    {
        StartCoroutine(LoadingScenesProgress());
    }

    private AsyncOperation _AsyOper;

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadingScenesProgress()
    {
        _AsyOper = SceneManager.LoadSceneAsync(sceneName);
        _FloProgressNumber = _AsyOper.progress;
        yield return _AsyOper;
    }

    private void Update()
    {
        Debug.Log(_FloProgressNumber);
        if (_FloProgressNumber <= 1)
        {
            _FloProgressNumber += 0.001f;
        }

        SliLoadingProgress.value = _FloProgressNumber;
    }
}