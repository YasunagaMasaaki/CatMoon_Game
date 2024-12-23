using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakSleep : MonoBehaviour
{
    // 睡眠状態
    private bool isSleep = false; 

    //敵情報
    private SpriteRenderer sr; 
    private Collider2D enemyCollider; 
    private Rigidbody2D rb; 

    //Light時間
    private float lightHitTime = 0f; // ライトが当たっている時間
    [SerializeField] float sleepTime = 1f; //　眠るまでに必要な時間

    [SerializeField] float attackRange = 1.5f; // 攻撃範囲の半径
    [SerializeField] float attackCooldown = 2f; // 攻撃間隔
    [SerializeField] LayerMask playerLayer; // プレイヤーのレイヤー

    private bool isPlayerInRange = false; // プレイヤーが範囲内にいるか
    private bool canAttack = true; // 攻撃可能か
    private Transform player; // プレイヤーの位置情報

    void Start()
    {
        //敵情報
        sr = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        // プレイヤーをシーンから探す
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
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

        // プレイヤーとの距離を測定して攻撃範囲内かを判定
        if (player == null) 
            return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer <= attackRange;

        // 範囲内にプレイヤーがいれば攻撃を実行
        if (isPlayerInRange && canAttack)
        {
            StartCoroutine(Attack());
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
        // 無力化時のアニメーションや効果音などを追加する場合はここで実装
        if (sr != null) 
            sr.color = Color.green;
        if (enemyCollider != null) 
            enemyCollider.enabled = false;
        if (rb != null) 
            rb.simulated = false;
    }

    //// 無力化解除（例えば一定時間後など）
    //void Enable()
    //{
    //    isDisabled = false;

    //    if (sr != null) sr.color = Color.white;
    //    if (enemyCollider != null) enemyCollider.enabled = true;
    //    if (rb != null) rb.simulated = true;
    //}

    private IEnumerator Attack()
    {
        // 無力化状態中は攻撃をスキップ
        if (isSleep) 
            yield break;

        canAttack = false;

        if (player != null)
            Destroy(player.gameObject);

        // 攻撃クールダウン
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    void OnDrawGizmosSelected()
    {
        // 攻撃範囲を可視化
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
