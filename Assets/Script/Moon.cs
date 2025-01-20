using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // プレイヤーと接触した場合
        {
            GameManager.instance.AddMoon(); // スコアを追加
            Destroy(gameObject); // オブジェクトを削除
        }
    }
}
