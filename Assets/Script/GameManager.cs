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
        // 初期表示
        UpdateScoreUI();
    }

    public void AddMoon()
    {
        moonCollected++;

        // スコアのUIを更新
        UpdateScoreUI();

        // すべて集めたらゲームクリア
        if (moonCollected >= totalMoon)
        {
            GameClear();
        }
    }

    private void UpdateScoreUI()
    {
        // テキストを "収集数 / 合計数" に更新
        scoreText.text = $"{moonCollected} / {totalMoon}";
    }

    private void GameClear()
    {
        Debug.Log("Game Clear!");
        // ゲームクリア時の処理を追加
        // 例: シーン切り替え、クリア画面表示など
    }
}
