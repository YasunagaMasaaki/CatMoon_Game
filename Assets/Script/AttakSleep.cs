using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakSleep : MonoBehaviour
{
    private Animator enemyAnim;

    // 睡眠状態
    private bool isSleep = false; 

    //敵情報 
    private Collider2D enemyCollider; 
    private Rigidbody2D rb; 

    //Light時間
    private float lightHitTime = 0f; // ライトが当たっている時間
    [SerializeField] float sleepTime = 1f; //　眠るまでに必要な時間

    void Start()
    {
        //敵情報
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        enemyAnim = GetComponent<Animator>();
    }

    void Update()
    {
        // 無力化中なら攻撃を無効化
        if (isSleep) return;

        // ライトが当たっている場合、睡眠状態に移行
        if (lightHitTime > 0)
        {
            lightHitTime += Time.deltaTime;

            if (lightHitTime >= sleepTime)
            {
                Sleep();
                return;
            }
        }
    }

    //Lightが当たってるかどうか
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light")) 
            lightHitTime = 0.01f;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light")) 
            lightHitTime = 0f; 
    }

    // 無力化処理
    void Sleep()
    {
        if (isSleep) 
            return; // すでに無力化されている場合はスキップ

        isSleep = true;
        
        if (enemyAnim != null)
            enemyAnim.SetBool("Sleep", true);
        if (enemyCollider != null) 
            enemyCollider.enabled = false;
        if (rb != null) 
            rb.simulated = false;
    }
}
