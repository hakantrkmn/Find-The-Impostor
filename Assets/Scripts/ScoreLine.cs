using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLine : MonoBehaviour
{
    public TextMeshProUGUI nickName;
    public TextMeshProUGUI level;

    private void Start()
    {
        var grid = GetComponent<GridLayoutGroup>();
        var rect = transform.parent.parent.GetComponent<RectTransform>();
        var cellSize = new Vector2(rect.rect.width / 2, rect.rect.height/5);
        grid.cellSize = cellSize;
    }
}
