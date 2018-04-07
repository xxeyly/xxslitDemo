using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_AutoSizeHight : MonoBehaviour
{
    [SerializeField] private Text Content;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(330, Content.preferredHeight+30);
    }
}