using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintArea : MonoBehaviour
{
    public GameObject hintPanel;  // UIパネル

    private bool isPlayerInRange = false;

    private bool hasTriggered = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            hintPanel.SetActive(false);
            Time.timeScale = 1f; // 時間を元に戻す
        }
    }

    // プレイヤーが範囲に入ったとき
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered) // Playerタグがある場合
        {
            hasTriggered = true;
            hintPanel.SetActive(true);
            isPlayerInRange = true;
            Time.timeScale = 0f; // 時間を止める
        }
    }

    // プレイヤーが範囲を出たとき
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
