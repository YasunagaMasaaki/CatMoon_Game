using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int moonCollected = 0; // ���݂̎��W��
    public int totalMoon = 5;      // �K�v�Ȏ��W��

    public Text scoreText;

    [SerializeField, Header("�ύX����G���x�[�^�[")]
    private GameObject movePoint;

    private Vector2 movePosition = new Vector2(73,35);

    [SerializeField, Header("�Q�[���I�[�o�[UI")]
    private GameObject gameOverUI;

    private GameObject player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();

        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Update()
    {
        ShowGameOverUI();
    }

    public void AddMoon()
    {
        int lastmoon = 4;

        moonCollected++;

        UpdateScoreUI();

        if (moonCollected == lastmoon)
        {
            Last();
        }
        if (moonCollected >= totalMoon)
        {
            GameClear();
        }
    }

    private void Last()
    {
       movePoint.transform.position = movePosition;
    }

    private void UpdateScoreUI()
    {
        // �e�L�X�g�� "���W�� / ���v��" �ɍX�V
        scoreText.text = $"{moonCollected}/{totalMoon}";
    }

    private void GameClear()
    {
        Debug.Log("Game Clear!");
        // �Q�[���N���A���̏�����ǉ�
        // ��: �V�[���؂�ւ��A�N���A��ʕ\���Ȃ�
    }

    private void ShowGameOverUI()
    {
        if (player != null) return;

        gameOverUI.SetActive(true);
    }
}
