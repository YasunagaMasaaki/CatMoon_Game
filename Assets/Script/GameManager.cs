using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int moonCollected = 0; // 現在の収集数
    public int totalMoon = 5;      // 必要な収集数

    public Text scoreText;

    [SerializeField, Header("変更するエレベーター")]
    private GameObject movePoint;

    private Vector2 movePosition = new Vector2(73.5f,35f);

    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject gameOverUI;

    private GameObject player;

    private bool bStart;
    private Fade fade;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();

        player = FindObjectOfType<PlayerController>().gameObject;

        bStart = false;
        fade = FindObjectOfType<Fade>();
        fade.FadeStart(GameStart);
    }

    private void GameStart()
    {
        bStart=true;
    }

    private void Update()
    {
        ShowGameOverUI();
    }

    public void AddMoon()
    {
        moonCollected++;

        UpdateScoreUI();

        if (moonCollected == 4)
        {
            Last();
        }
        if (moonCollected >= totalMoon)
        {
            GameClear();
        }
    }

    private void Last()
    {
       movePoint.transform.position = movePosition;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"{moonCollected}/{totalMoon}";
    }

    private void GameClear()
    {
        SceneManager.LoadScene("End");

        //if (bStart)
        //{
        //    fade.FadeStart(End);
        //    bStart = false;
        //}
    }

    private void End()
    {
        SceneManager.LoadScene("End");
    }

    private void ShowGameOverUI()
    {
        if (player != null) return;

        gameOverUI.SetActive(true);
    }
}
