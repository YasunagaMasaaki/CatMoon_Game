using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button manualButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button howButton;
    [SerializeField] private Button ruleButton;
    [SerializeField] private Button rule2Button;

    [SerializeField, Header("�{�^��")]
    private GameObject buttonPanel;
    [SerializeField, Header("�������")]
    private GameObject howPlayPanel;
    [SerializeField, Header("���[���P")]
    private GameObject rulePanel;
    [SerializeField, Header("���[���Q")]
    private GameObject rulePanel2;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
        manualButton.onClick.AddListener(OnManualButtonClick);
        closeButton.onClick.AddListener(OnCloseButtonClick);
        howButton.onClick.AddListener(OnHowButtonClick);
        ruleButton.onClick.AddListener(OnRuleButtonClick);
        rule2Button.onClick.AddListener(OnRule2ButtonClick);
    }

    // �Q�[���J�n�̏���
    void OnStartButtonClick()
    {
        Debug.Log("�Q�[�����J�n���܂��I");
        // ��: �V�[���J��
        // SceneManager.LoadScene("GameScene");
    }

    // �Q�[���I���̏���
    void OnExitButtonClick()
    {
        Debug.Log("�Q�[�����I�����܂��I");
        Application.Quit();
    }

    // �V�ѕ�������ʂ̏���
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
