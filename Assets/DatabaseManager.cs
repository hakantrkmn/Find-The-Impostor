using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference reference;

    
    private async void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.GetReference("users");

    }

    private void OnEnable()
    {
        EventManager.UpdateUser += UpdateUser;
        EventManager.CreateUser += CreateUser;
        EventManager.GetAllUsers += GetUsers;
    }

    private void UpdateUser(User obj)
    {
        UpdateUser(obj.userId,obj.nickName,obj.level);
    }

    private void OnDisable()
    {
        EventManager.UpdateUser -= UpdateUser;
        EventManager.CreateUser -= CreateUser;

    }

    private void CreateUser(string nick)
    {
        writeNewUser(nick,1);
    }

    [Button]
    public async  Task<List<User>> GetUsers()
    {
        List<User> allUsers = new List<User>();
        await reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                return allUsers;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;



                foreach (DataSnapshot user in snapshot.Children)
                {
                    User userA = JsonUtility.FromJson<User>(user.GetRawJsonValue());
                    allUsers.Add(userA);
                }


                return allUsers;
            }

            return allUsers;
        });

        return allUsers;
    }

    [Button]
    private async void writeNewUser(string name, int level)
    {
        User user = new User(name, level);
        var lastUserID = await GetLastId();
        if (lastUserID == -1)
        {
            lastUserID = 0;
        }
        else
        {
            lastUserID++;
        }

        user.userId = lastUserID;
        string json = JsonUtility.ToJson(user);
        ES3.Save("user",user);

        await reference.Child(lastUserID.ToString()).SetRawJsonValueAsync(json);
    }

    
    private void UpdateUser(int userID,string name, int level)
    {
        User user = new User(name, level);
        user.userId = userID;
        string json = JsonUtility.ToJson(user);

        reference.Child(userID.ToString()).SetRawJsonValueAsync(json);
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
}