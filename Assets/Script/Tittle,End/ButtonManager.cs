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

    [SerializeField, Header("�{�^���p�l��")]
    private GameObject buttonPanel;
    [SerializeField, Header("��������p�l��")]
    private GameObject howPlayPanel;
    [SerializeField, Header("���[���P�p�l��")]
    private GameObject rulePanel;
    [SerializeField, Header("���[���Q�p�l��")]
    private GameObject rulePanel2;
    [SerializeField, Header("�X�^�[�g��")]
    private GameObject startSE;
    [SerializeField, Header("�y�[�W��")]
    private GameObject nextPageSE;
    [SerializeField, Header("���鉹")]
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
    }
    //���ׂẴp�l����false��
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
