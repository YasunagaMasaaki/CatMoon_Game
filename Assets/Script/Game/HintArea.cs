using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintArea : MonoBehaviour
{
    [SerializeField,Header("ヒントパネル")]
    public GameObject hintPanel;
    [SerializeField, Header("ヒント音")]
    private GameObject hintSE;
    [SerializeField, Header("BGM")]
    private AudioSource bgm;
    private bool isPlayerInRange = false;
    private bool hasTriggered = false; //連続発動を阻止
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
            Time.timeScale = 1f; // 時間を元に戻す
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
            Time.timeScale = 0f; // 時間を止める
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
