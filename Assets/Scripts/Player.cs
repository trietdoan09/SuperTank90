using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    //tao vien dan
    [SerializeField] private GameObject bullet;

    Direction playerDirection;
    Vector2 direction;

    Vector2 tempDirection;

    StorageHelper helper;
    GameData gameData;
    ListGameData listGameData;
    void Start()
    {
        direction = new Vector2(0, 0);
        tempDirection = new Vector2(0, 1);
        //

        //ReadData();
        //ReadListData();
        ReadFromDB();

        //a little test
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Shoot();

        //SaveData();
        //SaveListData();

        // save data to sqlite
        SaveToDB();
    }
    void SaveToDB()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            GameDB db = new GameDB();
            db.Add(new GameEntity(-1, "user1", Random.Range(1,1000)));
            db.Add(new GameEntity(-1, "user2", Random.Range(1, 1000)));

        }
    }
    void ReadFromDB()
    {
        //lay data tu db
        GameDB db2 = new GameDB();
        //
        IDataReader reader = db2.GetAllData();
        List<GameEntity> myList = new List<GameEntity>();
        while(reader.Read())
        {
            GameEntity entity = new GameEntity(
                int.Parse(reader[0].ToString()),
                reader[1].ToString(),
                int.Parse(reader[2].ToString())
                );
            Debug.Log($"id: {entity.id} ==== username: {entity.username} ====point: {entity.points}");
            myList.Add(entity);
        };

    }
    void ReadData()
    {
        try
        {
            helper = new StorageHelper();
            helper.LoadData();
            gameData = helper.data;
            // nếu có data
            if(gameData != null)
            {
                Debug.Log($"Name: {gameData.name}, point: {gameData.points}");
            }
        }
        catch(System.Exception e)
        {
            Debug.Log($"Error game data: {e.Message}");
        }
        
    }
    void SaveData()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(gameData == null)
            {
                gameData = new GameData()
                {
                    name = "abc",
                    points = 10,
                    gameTime = 2.5f
                };
            }
            else
            {
                gameData.name = "223";
                gameData.points = 100;
                gameData.gameTime = 5.5f;
            }
            helper.data = gameData;
            helper.SaveData();
            Debug.Log("Save success");
        }
    }

    void ReadListData()
    {
        try
        {
            helper = new StorageHelper();
            helper.LoadListData();
            listGameData = helper.list;
            //lấy top 3 người chơi theo điểm
            //list = list.OrderByDescending(item => item.points).Skip(0).Take(3).ToList();
            //var result = (from item in list orderby item.points descending select item).Take(3).ToList();
            // nếu có data
            if (gameData != null && listGameData.list.Count>0)
            {
                Debug.Log($"Name: {listGameData.list[0]}, point: {listGameData.list[0]}");
            }
        }
        catch (System.Exception e)
        {
            Debug.Log($"Error read list game data: {e.Message}");
        }
    }
    void SaveListData()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            var data = new GameData
            {
                name = Random.Range(1, 10000).ToString(),
                points = Random.Range(1, 10000),
                gameTime = float.Parse(Random.Range(1, 10000).ToString())
            };
            listGameData.list.Add(data);
            helper.list = listGameData;
            helper.SaveListData();
        }
        Debug.Log("Save file list success");
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var _bullet = Instantiate(bullet);
            _bullet.transform.position = transform.position + (Vector3)tempDirection;
            _bullet.GetComponent<BulletController>().SetDirection(playerDirection);
            Destroy(_bullet, 0.5f);
        }
    }
    private void Moving()
    {
        direction = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direction = new Vector2(-1, 0);
            tempDirection = direction;
            transform.eulerAngles = new Vector3(0, 0, 90);
            playerDirection = Direction.Left;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction = new Vector2(1, 0);
            tempDirection = direction;
            transform.eulerAngles = new Vector3(0, 0, -90);
            playerDirection = Direction.Right;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            direction = new Vector2(0, 1);
            tempDirection = direction;
            transform.eulerAngles = new Vector3(0, 0, 0);
            playerDirection = Direction.Up;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            direction = new Vector2(0, -1);
            tempDirection = direction;
            transform.eulerAngles = new Vector3(0, 0, 180);
            playerDirection = Direction.Down;
        }
        Vector2 moveAmount = direction * Time.deltaTime * speed;
        transform.position += (Vector3)moveAmount;
    }

    
}
