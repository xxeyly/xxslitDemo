using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_HubBloodGroove : MonoBehaviour
{
    private Canvas HubBloodGroove;
    private Slider healthBar;

    private void Awake()
    {
        HubBloodGroove = transform.GetComponentInChildren<Canvas>();
        healthBar = HubBloodGroove.transform.Find("healthBar").GetComponent<Slider>();
        GetComponent<Ctrl_Enemy_Property>().eveEnemyBloodGroove += OnHealthBar;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnHealthBar(float value)
    {
        healthBar.value = value;
    }
}