using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private int quantity;
    private int currentEnemy;
    [SerializeField] private int minY, maxY, minX, maxX;

    [SerializeField] private GameObject[] positionSpawn;
    [SerializeField] private Sprite[] sprites;
    public int enemyType1, enemyType2, enemyType3;

    public int totalScore = 0;
    [SerializeField] private GameObject showPanelFinish;
    [SerializeField] private TextMeshProUGUI status;
    [SerializeField] private TextMeshProUGUI showScore;
    [SerializeField] private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        showPanelFinish.SetActive(false);
        enemyType1 = enemyType2 = enemyType3 = 0;
        quantity = Random.Range(1, 6);
        currentEnemy = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnemy <= quantity)
        {
            StartCoroutine(SpawnEnemy());
            currentEnemy += 1;
        }
        if(Time.timeScale==0)
        {
            ShowFinish();
        }
    }

    IEnumerator SpawnEnemy()
    {
        int randomSprite = Random.Range(0, sprites.Length);
        enemy.GetComponent<SpriteRenderer>().sprite = sprites[randomSprite];
        
        //CaculateEnemy(randomSprite);
        var _enemy = Instantiate(enemy);
        _enemy.GetComponent<Enemy>().typeEnemy = randomSprite;
        var random = Random.Range(0, 4);
        _enemy.transform.position = positionSpawn[random].transform.position;
        yield return new WaitForSeconds(1f);
    }

    public void CaculateEnemy(int x)
    {
        switch(x)
        {
            case 0:
                enemyType1++;
                break;
            case 1:
                enemyType2++;
                break;
            case 2:
                enemyType3++;
                break;
            default: break;
        }
    }
    public void DestroyEnemy(int x)
    {
        switch (x)
        {
            case 0:
                enemyType1++;
                break;
            case 1:
                enemyType2++;
                break;
            case 2:
                enemyType3++;
                break;
            default: break;
        }
    }
    void ShowFinish()
    {
        showPanelFinish.SetActive(true);
        status.text = "Complete";
        showScore.text = $"Total Score: "+totalScore.ToString();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
