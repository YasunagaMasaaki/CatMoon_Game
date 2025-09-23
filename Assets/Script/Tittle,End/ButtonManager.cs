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
    [SerializeField] private List<Button> otherButtons; // ����s�\�ɂ��������̃{�^������

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
        fade = FindObjectOfType<Fade>();
    }

    // �`���[�g���A���J�n�̏���
    public void OnTutorialButtonClick()
    {
        Instantiate(startSE);
        fade.FadeStart(TutorialStart);
    }
    private void TutorialStart()
    {
        SceneManager.LoadScene("Game");
    }

    // �Q�[���J�n�̏���
    public void OnStartButtonClick()
    {
        Instantiate(startSE);
        fade.FadeStart(GameStart);
    }
    private void GameStart()
    {
        SceneManager.LoadScene("GameMain");
    }

    // �Q�[���I���̏���
    public void OnExitButtonClick()
    {
        Debug.Log("�Q�[�����I�����܂��I");
        Application.Quit();
    }

    // �V�ѕ�������ʂ̏���
    public void OnManualButtonClick()
    {
        Instantiate(nextPageSE);
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
