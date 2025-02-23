using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button manualButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button howButton;
    [SerializeField] private Button ruleButton;
    [SerializeField] private Button rule2Button;

    [SerializeField, Header("ボタン")]
    private GameObject buttonPanel;
    [SerializeField, Header("操作説明")]
    private GameObject howPlayPanel;
    [SerializeField, Header("ルール１")]
    private GameObject rulePanel;
    [SerializeField, Header("ルール２")]
    private GameObject rulePanel2;

    private Fade fade;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
        manualButton.onClick.AddListener(OnManualButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
        howButton.onClick.AddListener(OnHowButtonClick);
        ruleButton.onClick.AddListener(OnRuleButtonClick);
        rule2Button.onClick.AddListener(OnRule2ButtonClick);

        fade = FindObjectOfType<Fade>();
    }

    // ゲーム開始の処理
    void OnStartButtonClick()
    {
        fade.FadeStart(GameStart);

    }
    private void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    // ゲーム終了の処理
    void OnExitButtonClick()
    {
        Debug.Log("ゲームを終了します！");
        Application.Quit();
    }

    // 遊び方説明画面の処理
    void OnManualButtonClick()
    {
        buttonPanel.SetActive(true);
        howPlayPanel.SetActive(true);
    }

    void OnCloseButtonClick()
    {
        buttonPanel.SetActive(false);
        howPlayPanel.SetActive(false);
        rulePanel.SetActive(false);
        rulePanel2.SetActive(false);
    }
    void OnHowButtonClick()
    {
        howPlayPanel.SetActive(true);
        rulePanel.SetActive(false);
        rulePanel2.SetActive(false);
    }
    void OnRuleButtonClick()
    {
        rulePanel.SetActive(true);
        howPlayPanel.SetActive(false);
        rulePanel2.SetActive(false);
    }
    void OnRule2ButtonClick()
    {
        rulePanel2.SetActive(true);
        howPlayPanel.SetActive(false);
        rulePanel.SetActive(false);
    }
}
