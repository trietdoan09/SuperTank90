using System;
using System.Collections.Generic;
public class GameData
{
    public string name;
    public int points;
    public float gameTime;
}

[Serializable]
public class ListGameData
{
    public List<GameData> list;
}
