using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ctrl_LoadScene : MonoBehaviour {

    public void OnNewGame()
    {
        SceneManager.LoadScene(1);
    }
}
