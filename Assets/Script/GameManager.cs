using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    private void Update()
    {
        int lastmoon = 4;

        ShowGameOverUI();

        if (moonCollected == lastmoon)
        {
            Last();
        }
    }

    public void AddMoon()
    {
        

        moonCollected++;

        UpdateScoreUI();

        
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
        Debug.Log("Game Clear!");
        // ゲームクリア時の処理を追加
        // 例: シーン切り替え、クリア画面表示など
    }

    private void ShowGameOverUI()
    {
        if (player != null) return;

        gameOverUI.SetActive(true);
    }
}
