using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public RectTransform rectTransform;

    public GameObject scoreLinePrefab;
    public List<ScoreLine> scoreLines;
    private void Start()
    {
        var cellSize = new Vector2(rectTransform.rect.width-20, rectTransform.rect.height / 6);
        gridLayoutGroup.cellSize = cellSize;
        GetUsers();
    }



    [Button]
    public async void GetUsers()
    {
        foreach (var scoreLine in scoreLines)
        {
            Destroy(scoreLine.gameObject);
        }
        scoreLines.Clear();
        var users = await EventManager.GetAllUsers();

        users = users.OrderBy(x => x.level).ToList();
        users.Reverse();
        foreach (var user in users)
        {
            var scoreLine = Instantiate(scoreLinePrefab, Vector3.zero, Quaternion.identity, gridLayoutGroup.transform).GetComponent<ScoreLine>();
            scoreLines.Add(scoreLine);
            scoreLine.nickName.text = user.nickName;
            scoreLine.level.text = user.level.ToString();
        }
    }
}
