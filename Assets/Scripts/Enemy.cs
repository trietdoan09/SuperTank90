using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Direction direction;
    [SerializeField] private Vector2 bulletDirection;
    Vector2 vectorDirection;
    [SerializeField] private float time;
    private float timer;

    [SerializeField] private float timeFiring;
    private float timerFiring;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject gameController;

    //type enemy
    public int typeEnemy;
    void Start()
    {
        bulletDirection = new Vector2(0, 1);
        timeFiring = 3f;
        timer = time = Random.Range(3, 5);
        timerFiring = timeFiring;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerFiring -= Time.deltaTime;
        if(timer<=0)
        {
            timer = time;
            //doi huong
            Rotation();
        }
        if(timerFiring<=0)
        {
            timerFiring = Random.Range(3, 10);
            // ban dan
            Firing();
        }
        Moving();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bound") || 
            collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("GroundCanBreak"))
        {
            Rotation();
        }
        if(collision.gameObject.CompareTag("Bullet"))
        {
            if(typeEnemy<1)
            {
                gameController.GetComponent<GameController>().totalScore += 1 * 100;
            }
            else
            {
                gameController.GetComponent<GameController>().totalScore += typeEnemy * 100;
            }
        }
    }

    private void Rotation()
    {
        var random = Random.Range(0, 4);
        var currentDirection = (int)direction;
        while (random == currentDirection)
        {
            random = Random.Range(0, 4);
        }
        switch (random)
        {
            case 0:
                direction = Direction.Up;
                break;
            case 1:
                direction = Direction.Down;
                break;
            case 2:
                direction = Direction.Left;
                break;
            case 3:
                direction = Direction.Right;
                break;
            default: break;
        }
    }
    private void Moving()
    {
        vectorDirection = new Vector2(0, 0);
        switch (direction)
        {
            case Direction.Up:
                {
                    vectorDirection = new Vector2(0, 1);
                    bulletDirection = vectorDirection;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                }
            case Direction.Down:
                {
                    vectorDirection = new Vector2(0, -1);
                    bulletDirection = vectorDirection;
                    transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                }
            case Direction.Left:
                {
                    vectorDirection = new Vector2(-1, 0);
                    bulletDirection = vectorDirection;
                    transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                }
            case Direction.Right:
                {
                    vectorDirection = new Vector2(1, 0);
                    bulletDirection = vectorDirection;
                    transform.eulerAngles = new Vector3(0, 0, -90);
                    break;
                }
            default: break;
        }
        Vector2 moveAmount = speed * Time.deltaTime * vectorDirection.normalized;
        transform.position += (Vector3)moveAmount;
    }
    public void SetDirection(Direction direction) => this.direction = direction;

    private void Firing()
    {
        var _bullet = Instantiate(bullet);
        _bullet.transform.position = transform.position + (Vector3)bulletDirection;
        _bullet.GetComponent<BulletController>().SetDirection(direction);
        Destroy(_bullet, 2f);
    }
}
