using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakSleep : MonoBehaviour
{
    private Animator enemyAnim;

    // �������
    private bool isSleep = false; 

    //�G��� 
    private Collider2D enemyCollider; 
    private Rigidbody2D rb;
    [SerializeField, Header("�U����")]
    private int attackPower;

    //Light����
    private float lightHitTime = 0f; // ���C�g���������Ă��鎞��
    [SerializeField] float sleepTime = 1f; //�@����܂łɕK�v�Ȏ���
    private float wakeUpTime = 5f;

    void Start()
    {
        //�G���
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        enemyAnim = GetComponent<Animator>();
    }

    void Update()
    {
        // ���͉����Ȃ�U���𖳌���
        if (isSleep) return;

        // ���C�g���������Ă���ꍇ�A������ԂɈڍs
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
        if (isSleep) 
            return; // ���łɖ��͉�����Ă���ꍇ�̓X�L�b�v

        isSleep = true;
        
        if (enemyAnim != null)
            enemyAnim.SetBool("Sleep", true);
        if (enemyCollider != null) 
            enemyCollider.enabled = false;
        if (rb != null) 
            rb.simulated = false;
        // �G�̌������Œ�i��F�E�����ɌŒ�j
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

    public void PlayerDamage(PlayerController player)
    {
        player.Damage(attackPower);
    }
}
