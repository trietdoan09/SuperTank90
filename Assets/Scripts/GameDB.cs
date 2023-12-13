using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class GameDB : SQLiteHelper
{
    public GameDB() : base()
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS Players(id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT, points INTEGER)"; ;
        dbcmd.ExecuteNonQuery();
    }

    public void Add(GameEntity entity)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = $"INSERT INTO Players(username, points) values('{entity.username}', {entity.points})";
        dbcmd.ExecuteNonQuery();
    }
    //select * from players
    public override IDataReader GetAllData()
    {
        return base.GetAllData("Players");
    }
    //update players set username = 'abc' where id =1
    public void Update(GameEntity entity)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = $"Update Players set username = '{entity.username}', points ={entity.points} where id = {entity.id}";
        dbcmd.ExecuteNonQuery();
    }
    //delete
    public void Delete(GameEntity entity)
    {
        IDbCommand dbcmd = GetDbCommand();
        dbcmd.CommandText = $"Update Players set username = '{entity.username}', points ={entity.points} where id = {entity.id}";
        dbcmd.ExecuteNonQuery();
    }
}
