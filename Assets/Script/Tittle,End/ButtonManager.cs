using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Fade fade;

    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button manualButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private List<Button> otherButtons; // 操作不能にしたい他のボタンたち

    [SerializeField, Header("ボタンパネル")]
    private GameObject buttonPanel;
    [SerializeField, Header("操作説明パネル")]
    private GameObject howPlayPanel;
    [SerializeField, Header("スタート音")]
    private GameObject startSE;
    [SerializeField, Header("ページ音")]
    private GameObject nextPageSE;
    [SerializeField, Header("閉じる音")]
    private GameObject closeSE;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);

        startButton.onClick.AddListener(OnStartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
        manualButton.onClick.AddListener(OnManualButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);

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

        // closeButton を選択状態に
        EventSystem.current.SetSelectedGameObject(closeButton.gameObject);

        // 他のボタンを無効にする
        foreach (var btn in otherButtons)
        {
            btn.interactable = false;
        }

        // closeButton だけ有効化
        closeButton.interactable = true;
    }
    private void OnCloseButtonClick()
    {
        Instantiate(closeSE);
        buttonPanel.SetActive(false);
        howPlayPanel.SetActive(false);

        foreach (var btn in otherButtons)
        {
            btn.interactable = true;
        }

        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
        EventSystem.current.SetSelectedGameObject(exitButton.gameObject);
        EventSystem.current.SetSelectedGameObject(manualButton.gameObject);

    }
}
