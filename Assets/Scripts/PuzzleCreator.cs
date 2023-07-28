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

    private DatabaseReference reference;
    public int levelIndex;

    private void Start()
    {
        CreatePuzzleCanvas();
        SetPuzzleColors();
         reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    [Button]
    public async void GetUsers()
    {
        var test = await GetLastId();
        Debug.Log(test);
        FirebaseDatabase dbInstance = FirebaseDatabase.DefaultInstance;
        dbInstance.GetReference("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("hata");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                

                foreach (DataSnapshot user in snapshot.Children)
                {
                    User userA = JsonUtility.FromJson<User>(user.GetRawJsonValue());
                    Debug.Log("" + userA.level + " - " + userA.nickName);
                }
                
            }
        });
    }
    public async Task<int> GetLastId()
    {
        int result = -1;
        FirebaseDatabase dbInstance = FirebaseDatabase.DefaultInstance;
        result = await dbInstance.GetReference("users").OrderByKey().LimitToLast(1).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("hata");
                return -1;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                
                Debug.Log(snapshot.Children.Count());
                
                var user = JsonUtility.FromJson<User>(snapshot.Children.Last().GetRawJsonValue());
                
                result = user.userId;
                Debug.Log(result);
                return result;

            }

            return 2;
        });
        

            return result;
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
        if (constraint == 1)
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
    private async void writeNewUser(string userId, string name, int level) {
        User user = new User(name, level);
        var lastUserID = await GetLastId();
        if (lastUserID==-1)
        {
            lastUserID = 0;
        }
        else
        {
            lastUserID++;
        }

        user.userId = lastUserID;
        string json = JsonUtility.ToJson(user);

        reference.Child("users").Child(lastUserID.ToString()).SetRawJsonValueAsync(json);
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

public class User
{
    public int userId;
    public string nickName;
    public int level;

    public User(string nickName, int level)
    {
        this.nickName = nickName;
        this.level = level;
    }
}