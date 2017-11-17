using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{

    public static void LoadScene(int level)
    {
        //Application.LoadLevel(level);
        SceneManager.LoadScene(level);
    }

}
