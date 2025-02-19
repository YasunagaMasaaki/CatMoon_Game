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

    [SerializeField] private Button reStart2Button;
    [SerializeField] private Button tittle2Button;
    [SerializeField] private Button closeButton;

    void Start()
    {
        reStartButton.onClick.AddListener(OnReStartButtonClick);
        tittleButton.onClick.AddListener(OnTittleButtonClick);
        reStart2Button.onClick.AddListener(OnReStart2ButtonClick);
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
        Time.timeScale = 0f; // ŽžŠÔ‚ðŽ~‚ß‚é
    }

    void OnReStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnTittleButtonClick()
    {
        SceneManager.LoadScene("Tittle");
    }
    void OnReStart2ButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnTittle2ButtonClick()
    {
        SceneManager.LoadScene("Tittle");
    }
    void OnCloseButtonClick()
    {
        //isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // ŽžŠÔ‚ðŒ³‚É–ß‚·
    }
}
