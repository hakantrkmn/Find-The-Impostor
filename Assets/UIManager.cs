using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_InputField nickInput;


    public TextMeshProUGUI nickText;
    public GameObject nickPanel;
    public TextMeshProUGUI levelText;
    public GameObject infoPanel;

    private void Start()
    {
        levelText.text = "level "+ES3.Load("level").ToString();
        if (ES3.KeyExists("nick"))
        {
            LoadNickname();
        }
        else
        {
            nickPanel.SetActive(true);

        }
    }

    public void SetNick()
    {
        var nick = nickInput.text;
        ES3.Save("nick",nick);
        LoadNickname();
    }


    public void LoadNickname()
    {
        nickPanel.SetActive(false);
        nickText.text = ES3.Load("nick").ToString();
        infoPanel.SetActive(true);
    }
}
