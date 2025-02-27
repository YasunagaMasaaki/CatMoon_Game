using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Fade fade;

    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button manualButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button howButton;
    [SerializeField] private Button ruleButton;
    [SerializeField] private Button rule2Button;

    [SerializeField, Header("ボタンパネル")]
    private GameObject buttonPanel;
    [SerializeField, Header("操作説明パネル")]
    private GameObject howPlayPanel;
    [SerializeField, Header("ルール１パネル")]
    private GameObject rulePanel;
    [SerializeField, Header("ルール２パネル")]
    private GameObject rulePanel2;
    [SerializeField, Header("スタート音")]
    private GameObject startSE;
    [SerializeField, Header("ページ音")]
    private GameObject nextPageSE;
    [SerializeField, Header("閉じる音")]
    private GameObject closeSE;

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
    private void OnStartButtonClick()
    {
        Instantiate(startSE);
        fade.FadeStart(GameStart);
    }
    private void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    // ゲーム終了の処理
    private void OnExitButtonClick()
    {
        Debug.Log("ゲームを終了します！");
        Application.Quit();
    }

    // 遊び方説明画面の処理
    private void OnManualButtonClick()
    {
        Instantiate(nextPageSE);
        buttonPanel.SetActive(true);
        howPlayPanel.SetActive(true);
    }
    //すべてのパネルをfalseに
    private void HideAllPanels()
    {
        howPlayPanel.SetActive(false);
        rulePanel.SetActive(false);
        rulePanel2.SetActive(false);
    }

    private void OnCloseButtonClick()
    {
        Instantiate(closeSE);
        buttonPanel.SetActive(false);
        HideAllPanels();
    }

    private void OnHowButtonClick()
    {
        Instantiate(nextPageSE);
        HideAllPanels();
        howPlayPanel.SetActive(true);
    }

    private void OnRuleButtonClick()
    {
        Instantiate(nextPageSE);
        HideAllPanels();
        rulePanel.SetActive(true);
    }

    private void OnRule2ButtonClick()
    {
        Instantiate(nextPageSE);
        HideAllPanels();
        rulePanel2.SetActive(true);
    }
}
