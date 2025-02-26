using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakSleep : MonoBehaviour
{
    private Collider2D enemyCollider;
    private Rigidbody2D rb;
    private Animator enemyAnim;
   
    [SerializeField, Header("攻撃力")]
    private int attackPower;
    [SerializeField, Header("眠るまでに必要な時間")]
    private float sleepTime;
    private float wakeUpTime = 5f;
    private float lightHitTime = 0f;
    private bool isSleep = false;
    [SerializeField, Header("寝る音")]
    private GameObject sleepSE;

    void Start()
    {
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
    }

    void Update()
    {
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

    void Sleep()
    {
        if (isSleep)  return;

        Instantiate(sleepSE);
        isSleep = true;
        
        if (enemyAnim != null)
            enemyAnim.SetBool("Sleep", true);
        if (enemyCollider != null) 
            enemyCollider.enabled = false;
        if (rb != null) 
            rb.simulated = false;
        // 睡眠中敵の向きを右に固定
        transform.localScale = new Vector3
            (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        Invoke("WakeUp", wakeUpTime);
    }

    void WakeUp()
    {
        isSleep = false;

        if (enemyAnim != null)
            enemyAnim.SetBool("Sleep", false);
        if (enemyCollider != null)
            enemyCollider.enabled = true;
        if (rb != null)
            rb.simulated = true;
    }

    public void PlayerDamage(PlayerController player,Vector2 knockbackDir)
    {
        player.Damage(attackPower, knockbackDir);
    }
}
