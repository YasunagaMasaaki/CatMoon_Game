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
        // �����\��
        UpdateScoreUI();
    }

    public void AddMoon()
    {
        moonCollected++;

        // �X�R�A��UI���X�V
        UpdateScoreUI();

        // ���ׂďW�߂���Q�[���N���A
        if (moonCollected >= totalMoon)
        {
            GameClear();
        }
    }

    private void UpdateScoreUI()
    {
        // �e�L�X�g�� "���W�� / ���v��" �ɍX�V
        scoreText.text = $"{moonCollected} / {totalMoon}";
    }

    private void GameClear()
    {
        Debug.Log("Game Clear!");
        // �Q�[���N���A���̏�����ǉ�
        // ��: �V�[���؂�ւ��A�N���A��ʕ\���Ȃ�
    }
}
