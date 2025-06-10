using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameButtonManager : MonoBehaviour
{
    [SerializeField] private Button reStartButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject pausePanel;

    private GameObject player;


    [SerializeField, Header("スタート音")]
    private GameObject startSE;
    [SerializeField, Header("閉じる音")]
    private GameObject closeSE;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        player.SetActive(false);
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(closeButton.gameObject);
        Time.timeScale = 0f; // 時間を止める
    }
    public void OnCloseButtonClick()
    {
        Instantiate(closeSE);
        player.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // 時間を元に戻す
    }
    public void OnReStartButtonClick()
    {
        Time.timeScale = 1f; // 時間を元に戻す
        Instantiate(startSE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnTittleButtonClick()
    {
        Instantiate(startSE);
        Time.timeScale = 1f; // 時間を元に戻す
        SceneManager.LoadScene("Tittle");
    }
}
