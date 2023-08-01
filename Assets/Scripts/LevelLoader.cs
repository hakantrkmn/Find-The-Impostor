using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        if (ES3.KeyExists("user"))
        {
            ES3.Save("level",(ES3.Load("user")as User).level);
            SceneManager.LoadScene(1);
        }
        else
        {
            ES3.Save("level",1);
            SceneManager.LoadScene(1);
        }
    }
    
}
