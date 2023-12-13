using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity
{
    public int id;
    public string username;
    public int points;

    public GameEntity(int id, string name, int points)
    {
        this.id = id;
        this.username = name;
        this.points = points;
    }
}
