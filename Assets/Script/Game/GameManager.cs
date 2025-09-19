using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject player;
    [SerializeField] private Fade fade;
    [SerializeField] private GameObject fadePanel;

    [SerializeField,Header("必要な収集数")]
    public int totalMoon;
    private int moonCollected = 0; // 現在の収集数

    [SerializeField] private string scenes;

    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject gameOverUI;
    [SerializeField] private Button reStartButton;
    [SerializeField, Header("スコアUI")]
    public Text scoreText;

    [SerializeField, Header("ゲームオーバー音")]
    private GameObject gameOverSE;
    [SerializeField, Header("BGM")]
    private AudioSource bgm;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        player.GetComponent<PlayerController>().enabled = false;
        fadePanel.SetActive(true);
        fade = FindObjectOfType<Fade>();
        fade.FadeStart(GameStart);
    }

    private void GameStart()
    {
        player.GetComponent<PlayerController>().enabled = true;
        UpdateScoreUI();
    }

    private void Update()
    {
        ShowGameOverUI();

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void AddMoon()
    {
        moonCollected++;

        UpdateScoreUI();

        if (moonCollected >= totalMoon)
        {
            fade.FadeStart(End);
            player.GetComponent<PlayerController>().enabled = false;
        }
    }
    private void UpdateScoreUI()
    {
        scoreText.text = $"{moonCollected}/{totalMoon}";
    }

    private void End()
    {
        SceneManager.LoadScene(scenes);
    }

    private void ShowGameOverUI()
    {
        if (player != null || gameOverUI.activeSelf) return;

        gameOverUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(reStartButton.gameObject);
        bgm.Stop();
        Instantiate(gameOverSE);
    }
}
