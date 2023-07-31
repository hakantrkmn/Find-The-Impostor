
using System;

[Serializable]
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