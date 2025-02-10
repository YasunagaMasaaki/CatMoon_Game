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
        ShowGameOverUI();
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

    private void UpdateScoreUI()
    {
        // テキストを "収集数 / 合計数" に更新
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
