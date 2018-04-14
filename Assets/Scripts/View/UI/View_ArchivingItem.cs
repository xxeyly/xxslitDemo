using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_ArchivingItem : MonoBehaviour
{
    [SerializeField] private Text index;
    public void UpdateIndex(string value)
    {
        index.text = value;
    }

 
}