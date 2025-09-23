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
    [SerializeField] private Button closeButton;
    [SerializeField] private List<Button> otherButtons; // 操作不能にしたい他のボタンたち

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
        fade = FindObjectOfType<Fade>();
    }

    // チュートリアル開始の処理
    public void OnTutorialButtonClick()
    {
        Instantiate(startSE);
        fade.FadeStart(TutorialStart);
    }
    private void TutorialStart()
    {
        SceneManager.LoadScene("Game");
    }

    // ゲーム開始の処理
    public void OnStartButtonClick()
    {
        Instantiate(startSE);
        fade.FadeStart(GameStart);
    }
    private void GameStart()
    {
        SceneManager.LoadScene("GameMain");
    }

    // ゲーム終了の処理
    public void OnExitButtonClick()
    {
        Debug.Log("ゲームを終了します！");
        Application.Quit();
    }

    // 遊び方説明画面の処理
    public void OnManualButtonClick()
    {
        Instantiate(nextPageSE);
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
    public void OnCloseButtonClick()
    {
        Instantiate(closeSE);
        howPlayPanel.SetActive(false);

        foreach (var btn in otherButtons)
        {
            btn.interactable = true;
        }

        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }

    public void OnTittleButtonClick()
    {
        Instantiate(startSE);
        fade.FadeStart(TittleScene);
    }
    private void TittleScene()
    {
        SceneManager.LoadScene("Tittle");
    }
}
