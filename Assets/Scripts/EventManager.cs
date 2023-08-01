using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class EventManager
{

    public static Func<Task<List<User>>> GetAllUsers;
    public static Action<string> CreateUser;
    public static Action<User> UpdateUser;
    public static Action<bool> LevelSuccess;

}
