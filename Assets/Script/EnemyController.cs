using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isDisabled = false; // 無力化状態
    private SpriteRenderer spriteRenderer; // 敵のスプライトレンダラー
    private Collider2D enemyCollider; // 敵のコライダー
    private Rigidbody2D rb; // 敵のリジッドボディ
    private float lightExposureTime = 0f; // ライトが当たっている時間
    [SerializeField] float requiredExposureTime = 3f; // 無力化に必要な時間

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDisabled)
        {
            // ここに無力化中の行動を追加
            return;
        }
        // ここに敵の通常の動きを追加
    }

    // ライトが当たり続けた場合の処理
    public void OnTriggerStay2D(Collider2D other)
    {
        if (isDisabled) return;

        // プレイヤーのライトに当たっている場合
        if (other.CompareTag("Light"))
        {
            lightExposureTime += Time.deltaTime;

            // ライトが当たり続けて必要時間に達したら無力化
            if (lightExposureTime >= requiredExposureTime)
            {
                Disable();
            }
        }
    }

    // ライトの範囲外に出た場合の処理
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            lightExposureTime = 0f; // 当たり続けた時間をリセット
        }
    }

    // 無力化処理
    public void Disable()
    {
        if (isDisabled) return; // すでに無力化されている場合はスキップ

        isDisabled = true;
        // 無力化時のアニメーションや効果音などを追加する場合はここで実装

        if (spriteRenderer != null) spriteRenderer.color = Color.green;
        if (enemyCollider != null) enemyCollider.enabled = false;
        if (rb != null) rb.simulated = false;
    }

    // 無力化解除（例えば一定時間後など）
    public void Enable()
    {
        isDisabled = false;

        if (spriteRenderer != null) spriteRenderer.color = Color.white;
        if (enemyCollider != null) enemyCollider.enabled = true;
        if (rb != null) rb.simulated = true;
    }
}
