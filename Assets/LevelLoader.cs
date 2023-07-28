using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        if (ES3.KeyExists("level"))
        {
            SceneManager.LoadScene((int)ES3.Load("level"));
        }
        else
        {
            ES3.Save("level",1);
            SceneManager.LoadScene(1);
        }
    }
    
}
