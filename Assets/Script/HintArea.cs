using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintArea : MonoBehaviour
{
    public GameObject hintPanel;  // UI�p�l��

    private bool isPlayerInRange = false;

    private bool hasTriggered = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            hintPanel.SetActive(false);
            Time.timeScale = 1f; // ���Ԃ����ɖ߂�
        }
    }

    // �v���C���[���͈͂ɓ������Ƃ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered) // Player�^�O������ꍇ
        {
            hasTriggered = true;
            hintPanel.SetActive(true);
            isPlayerInRange = true;
            Time.timeScale = 0f; // ���Ԃ��~�߂�
        }
    }

    // �v���C���[���͈͂��o���Ƃ�
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
