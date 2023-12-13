using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up, Down, Left, Right
}
public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private bool explore;
    private Animator animator;

    Direction direction;
    public bool status;
    public bool finish;
    [SerializeField] private GameObject[] item;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject gameController;
    //
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        animator = GetComponent<Animator>();
        //gameController = GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 _direction = new Vector2(0, 0);
        switch(direction)
        {
            case Direction.Up:
                {
                    _direction = new Vector2(0, 1);
                    transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                }
            case Direction.Down:
                {
                    _direction = new Vector2(0, -1);
                    transform.eulerAngles = new Vector3(0, 0, -90);
                    break;
                }
            case Direction.Left:
                {
                    _direction = new Vector2(-1, 0);
                    transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                }
            case Direction.Right:
                {
                    _direction = new Vector2(1, 0);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                }
            default: break;
        }
        Vector2 moveAmount = speed * Time.deltaTime * _direction.normalized;
        transform.position += (Vector3)moveAmount;
    }

    public void SetDirection(Direction direction) => this.direction = direction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("GroundCanBreak") || collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isExplore", true);
            Destroy(this, 0.05f);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            var type = enemy.GetComponent<Enemy>().typeEnemy;
            Debug.Log($"Type: " + type);
            gameController.GetComponent<GameController>().DestroyEnemy(type);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            finish = true;
            status = true;
            Time.timeScale = 0;
        }
        if (collision.gameObject.CompareTag("Close"))
        {
            finish = true;
            status = false;
            Time.timeScale = 0;
        }
    }
}
