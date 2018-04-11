using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View_NpcTopName : MonoBehaviour
{
    [SerializeField] private float visualDistance; //可视距离
    [SerializeField] private string Name;

    // Update is called once per frame
    void Update()
    {
        if (visualDistance >= Vector3.Distance(this.transform.position, Camera.main.transform.position))
        {
            GetComponent<TextMesh>().text = Name;
        }
        else
        {
            GetComponent<TextMesh>().text = "";
        }
    }
}