using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public Animator animacionPlayer;
    public GameObject GameOverText;
    private bool m_Started = false;
    private int m_Points;    
    private bool m_GameOver = false;

    public Text BestScore;
    public Text ScoreText;    
    private string nameBest;
    private string nameActual;
    private string scoreBest;
      
    void Start()
    {
        nameActual = MenuManager.Instance.actualName;        
        
        if (MenuManager.Instance.bestName != "")
        {
            nameBest = MenuManager.Instance.bestName;
        }
        
        scoreBest = MenuManager.Instance.bestScore.ToString();
        BestScore.text = "Best Score: " + nameBest + " : " + scoreBest;

        animacionPlayer.Play("TransicionMenu");
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        MenuManager.Instance.actualScore = m_Points;

        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                animacionPlayer.Play("TransicionAescena");
                StartCoroutine(SalirMenu());                
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{nameActual} Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (MenuManager.Instance.actualScore > MenuManager.Instance.bestScore)
        {
            MenuManager.Instance.bestScore = MenuManager.Instance.actualScore;
            MenuManager.Instance.bestName = nameActual;
        }
    }
    private IEnumerator SalirMenu()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
