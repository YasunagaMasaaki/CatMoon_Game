using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIButton : MonoBehaviour
{
    [SerializeField] private Button reStartButton;
    [SerializeField] private Button tittleButton;

    [SerializeField] private GameObject pausePanel;
    //private bool isPaused = false;

    [SerializeField] private Button tittle2Button;
    [SerializeField] private Button closeButton;

    [SerializeField, Header("スタート音")]
    private GameObject startSE;
    [SerializeField, Header("閉じる音")]
    private GameObject closeSE;

    void Start()
    {
        reStartButton.onClick.AddListener(OnReStartButtonClick);
        tittleButton.onClick.AddListener(OnTittleButtonClick);
        tittle2Button.onClick.AddListener(OnTittle2ButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // 時間を止める
    }

    void OnReStartButtonClick()
    {
        Instantiate(startSE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnTittleButtonClick()
    {
        Instantiate(startSE);
        SceneManager.LoadScene("Tittle");
    }
    void OnTittle2ButtonClick()
    {
        Instantiate(startSE);
        SceneManager.LoadScene("Tittle");
    }
    void OnCloseButtonClick()
    {
        Instantiate(closeSE);
        //isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // 時間を元に戻す
    }
}
