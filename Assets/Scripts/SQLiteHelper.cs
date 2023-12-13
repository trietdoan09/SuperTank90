using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

public class SQLiteHelper 
{
    private const string Tag = "SQLiteHelper:\t";

    private const string database_name = "Game3Db";

    public string db_connection_string;
    public IDbConnection db_connection;

    public SQLiteHelper()
    {
        db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
        db_connection = new SqliteConnection(db_connection_string);
        db_connection.Open();
    }

    ~SQLiteHelper()
    {
        db_connection.Close();
    }
    
    public virtual IDataReader GetAllData()
    {
        throw null;
    }

    public IDbCommand GetDbCommand()
    {
        return db_connection.CreateCommand();
    }
    //lay toan bo du lieu trong bang
    public IDataReader GetAllData(string table_name)
    {
        IDbCommand dbcmd = db_connection.CreateCommand();
        dbcmd.CommandText =
            "SELECT * FROM " + table_name;
        IDataReader reader = dbcmd.ExecuteReader();
        return reader;
    }

    public void Close()
    {
        db_connection.Close();
    }
}
