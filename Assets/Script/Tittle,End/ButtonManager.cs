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
    [SerializeField] private List<Button> otherButtons; // ����s�\�ɂ��������̃{�^������

    [SerializeField, Header("�{�^���p�l��")]
    private GameObject buttonPanel;
    [SerializeField, Header("��������p�l��")]
    private GameObject howPlayPanel;
    [SerializeField, Header("�X�^�[�g��")]
    private GameObject startSE;
    [SerializeField, Header("�y�[�W��")]
    private GameObject nextPageSE;
    [SerializeField, Header("���鉹")]
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

    // �Q�[���J�n�̏���
    private void OnStartButtonClick()
    {
        Instantiate(startSE);
        fade.FadeStart(GameStart);
    }
    private void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    // �Q�[���I���̏���
    private void OnExitButtonClick()
    {
        Debug.Log("�Q�[�����I�����܂��I");
        Application.Quit();
    }

    // �V�ѕ�������ʂ̏���
    private void OnManualButtonClick()
    {
        Instantiate(nextPageSE);
        buttonPanel.SetActive(true);
        howPlayPanel.SetActive(true);

        // closeButton ��I����Ԃ�
        EventSystem.current.SetSelectedGameObject(closeButton.gameObject);

        // ���̃{�^���𖳌��ɂ���
        foreach (var btn in otherButtons)
        {
            btn.interactable = false;
        }

        // closeButton �����L����
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
