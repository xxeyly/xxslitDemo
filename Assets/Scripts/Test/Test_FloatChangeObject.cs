using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_FloatChangeObject : MonoBehaviour
{

    [SerializeField]private Button TextButton;
	void Start ()
	{
	    float f = float.Parse(TextButton.tag);
        Debug.Log(f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
