using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintArea : MonoBehaviour
{
    [SerializeField,Header("�q���g�p�l��")]
    public GameObject hintPanel;
    [SerializeField, Header("�q���g��")]
    private GameObject hintSE;
    [SerializeField, Header("BGM")]
    private AudioSource bgm;
    private bool isPlayerInRange = false;
    private bool hasTriggered = false; //�A��������j�~
    public bool IsHintPanelActive()
    {
        return hintPanel.activeSelf;
    }
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            hintPanel.SetActive(false);
            bgm.Play();
            Time.timeScale = 1f; // ���Ԃ����ɖ߂�
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            bgm.Stop();
            Instantiate(hintSE);
            hasTriggered = true;
            hintPanel.SetActive(true);
            isPlayerInRange = true;
            Time.timeScale = 0f; // ���Ԃ��~�߂�
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
