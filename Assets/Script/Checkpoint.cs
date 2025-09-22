using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                // プレイヤーのリスポーン地点を更新
                player.SetCheckpoint(transform.position);
                Debug.Log("チェックポイント更新！");
            }
        }
    }
}
