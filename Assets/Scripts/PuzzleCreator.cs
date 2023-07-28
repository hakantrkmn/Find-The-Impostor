using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PuzzleCreator : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public RectTransform rectTransform;
    public Dot dotPrefab;
    public List<Dot> dots;


    public int levelIndex;

    private void Start()
    {
        CreatePuzzleCanvas();
        SetPuzzleColors();
    }

    [Button]
    public void CreatePuzzleCanvas()
    {
        //levelIndex = (int)ES3.Load("level") + 1;
        foreach (var dot in dots)
        {
            DestroyImmediate(dot.gameObject);
        }

        dots.Clear();
        var constraint = Mathf.FloorToInt(Mathf.Sqrt((float)levelIndex));
        if (constraint==1)
        {
            constraint = 2;
        }

        gridLayoutGroup.constraintCount = constraint;
        var cellSize = new Vector2((rectTransform.rect.width / constraint) - 50,
            (rectTransform.rect.width / constraint) - 50);

        gridLayoutGroup.cellSize = cellSize;

        for (int i = 0; i < levelIndex; i++)
        {
            var dot = Instantiate(dotPrefab.gameObject, transform.position, quaternion.identity, transform);
            dots.Add(dot.GetComponent<Dot>());
        }
    }


    [Button]
    public void SetPuzzleColors()
    {
        var color = Random.ColorHSV();

        var randDot = dots[Random.Range(0, dots.Count)];

        foreach (var dot in dots)
        {
            dot.image.color = color;
        }

        var hardness = levelIndex * .005f;
        if (Math.Abs(hardness - .3f) < .1f)
        {
            hardness = .24f;
        }
        color.r *= (.7f + hardness);
        color.g *= (.7f + hardness);
        color.b *= (.7f + hardness);
        randDot.image.color = color;
        randDot.isDifferent = true;
    }
}