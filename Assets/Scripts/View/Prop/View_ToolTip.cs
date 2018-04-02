using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class View_ToolTip : MonoBehaviour {
    [SerializeField]
    private Text toolTipText;
    [SerializeField]
    private Text contentText;
    private CanvasGroup canvasGroup;



    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
    }

    public void Show(string text)
    {
        toolTipText.text = text;
        contentText.text = text;
        canvasGroup.alpha = 1;
    }
    public void Hide()
    {
        canvasGroup.alpha = 0;
    }
    public void SetLocalPotion(Vector3 position)
    {
        transform.localPosition = position;
    }
	
}
