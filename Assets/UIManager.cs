using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TMP_InputField nickInput;


    public TextMeshProUGUI nickText;
    public GameObject nickPanel;
    public TextMeshProUGUI levelText;
    public GameObject infoPanel;

    public GameObject leaderBoardPanel;

    public GameObject levelWinPanel;
    public GameObject levelLosePanel;

    private void Start()
    {
        levelText.text = "level "+ES3.Load("level").ToString();
        if (ES3.KeyExists("user"))
        {
            LoadNickname();
        }
        else
        {
            nickPanel.SetActive(true);

        }
    }

    private void OnEnable()
    {
        EventManager.LevelSuccess += LevelSuccess;
    }

    private void OnDisable()
    {
        EventManager.LevelSuccess -= LevelSuccess;
    }

    private void LevelSuccess(bool obj)
    {
        levelWinPanel.SetActive(obj);
        levelLosePanel.SetActive(!obj);
    }

    [Button]
    public void NextLevel()
    {
        var user = (ES3.Load("user") as User);
        user.level++;
        ES3.Save("user",user);
        ES3.Save("level",user.level);
        EventManager.UpdateUser(user);
        SceneManager.LoadScene(1);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void SetNick()
    {
        var nick = nickInput.text;
        EventManager.CreateUser(nick);
        LoadNickname(nick);
    }

    public void OpenLeaderBoard()
    {
        if (leaderBoardPanel.activeSelf)
        {
            leaderBoardPanel.SetActive(false);
        }
        else
        {
            leaderBoardPanel.SetActive(true);

        }
    }
    public void LoadNickname(string nick)
    {
        nickPanel.SetActive(false);
        nickText.text = nick;
        infoPanel.SetActive(true);
    }
    public void LoadNickname()
    {
        nickPanel.SetActive(false);
        nickText.text = (ES3.Load("user") as User).nickName;
        infoPanel.SetActive(true);
    }
}
