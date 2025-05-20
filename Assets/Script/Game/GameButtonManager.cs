using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameButtonManager : MonoBehaviour
{
    [SerializeField] private Button reStartButton;
    [SerializeField] private Button tittleButton;

    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Button reStart2Button;
    [SerializeField] private Button tittle2Button;
    [SerializeField] private Button closeButton;

    [SerializeField, Header("�X�^�[�g��")]
    private GameObject startSE;
    [SerializeField, Header("���鉹")]
    private GameObject closeSE;

    void Start()
    {
        reStartButton.onClick.AddListener(OnReStartButtonClick);
        reStart2Button.onClick.AddListener(OnReStart2ButtonClick);
        tittleButton.onClick.AddListener(OnTittleButtonClick);
        tittle2Button.onClick.AddListener(OnTittle2ButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
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
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // ���Ԃ��~�߂�
    }
    private void OnCloseButtonClick()
    {
        Instantiate(closeSE);
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // ���Ԃ����ɖ߂�
    }

    private void Rstaet()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnReStartButtonClick()
    {
        Instantiate(startSE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnReStart2ButtonClick()
    {
        Time.timeScale = 1f; // ���Ԃ����ɖ߂�
        Instantiate(startSE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnTittleButtonClick()
    {
        Instantiate(startSE);
        SceneManager.LoadScene("Tittle");
    }
    private void OnTittle2ButtonClick()
    {
        Instantiate(startSE);
        Time.timeScale = 1f; // ���Ԃ����ɖ߂�
        SceneManager.LoadScene("Tittle");
    }
    
}
