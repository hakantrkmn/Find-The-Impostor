using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
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
    public List<Color> puzzleColors;

    public int levelIndex;

    private void Start()
    {
        CreatePuzzleCanvas();
        SetPuzzleColors();
    }

   
   
    
    [Button]
    public void CreatePuzzleCanvas()
    {
        levelIndex = (int)ES3.Load("level") ;
        foreach (var dot in dots)
        {
            DestroyImmediate(dot.gameObject);
        }

        dots.Clear();
        var constraint = Mathf.FloorToInt(Mathf.Sqrt((float)levelIndex));
        if (constraint == 1)
        {
            constraint = 2;
        }

        var minWidth = ((rectTransform.rect.width - 50) / constraint) - 50;
        var minHeight = ((rectTransform.rect.height-50) / (levelIndex/constraint)) - 50;
        gridLayoutGroup.constraintCount = constraint;
        if (minWidth< minHeight)
        {
            
            var cellSize = new Vector2(minWidth, minWidth);

            gridLayoutGroup.cellSize = cellSize;
        }
        else
        {
            var cellSize = new Vector2(minHeight, minHeight);

            gridLayoutGroup.cellSize = cellSize;
        }
       

        for (int i = 0; i < levelIndex; i++)
        {
            var dot = Instantiate(dotPrefab.gameObject, transform.position, quaternion.identity, transform);
            dots.Add(dot.GetComponent<Dot>());
        }
    }

    
    [Button]
    public void SetPuzzleColors()
    {
        var color = puzzleColors[Random.Range(0, puzzleColors.Count)];

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


