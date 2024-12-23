using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakSleep : MonoBehaviour
{
    // �������
    private bool isSleep = false; 

    //�G���
    private SpriteRenderer sr; 
    private Collider2D enemyCollider; 
    private Rigidbody2D rb; 

    //Light����
    private float lightHitTime = 0f; // ���C�g���������Ă��鎞��
    [SerializeField] float sleepTime = 1f; //�@����܂łɕK�v�Ȏ���

    [SerializeField] float attackRange = 1.5f; // �U���͈͂̔��a
    [SerializeField] float attackCooldown = 2f; // �U���Ԋu
    [SerializeField] LayerMask playerLayer; // �v���C���[�̃��C���[

    private bool isPlayerInRange = false; // �v���C���[���͈͓��ɂ��邩
    private bool canAttack = true; // �U���\��
    private Transform player; // �v���C���[�̈ʒu���

    void Start()
    {
        //�G���
        sr = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        // �v���C���[���V�[������T��
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
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

        // �v���C���[�Ƃ̋����𑪒肵�čU���͈͓����𔻒�
        if (player == null) 
            return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer <= attackRange;

        // �͈͓��Ƀv���C���[������΍U�������s
        if (isPlayerInRange && canAttack)
        {
            StartCoroutine(Attack());
        }
            
    }

    //Light���������Ă邩�ǂ���
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

    // ���͉�����
    void Sleep()
    {
        if (isSleep) 
            return; // ���łɖ��͉�����Ă���ꍇ�̓X�L�b�v

        isSleep = true;
        // ���͉����̃A�j���[�V��������ʉ��Ȃǂ�ǉ�����ꍇ�͂����Ŏ���
        if (sr != null) 
            sr.color = Color.green;
        if (enemyCollider != null) 
            enemyCollider.enabled = false;
        if (rb != null) 
            rb.simulated = false;
    }

    //// ���͉������i�Ⴆ�Έ�莞�Ԍ�Ȃǁj
    //void Enable()
    //{
    //    isDisabled = false;

    //    if (sr != null) sr.color = Color.white;
    //    if (enemyCollider != null) enemyCollider.enabled = true;
    //    if (rb != null) rb.simulated = true;
    //}

    private IEnumerator Attack()
    {
        // ���͉���Ԓ��͍U�����X�L�b�v
        if (isSleep) 
            yield break;

        canAttack = false;

        if (player != null)
            Destroy(player.gameObject);

        // �U���N�[���_�E��
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    void OnDrawGizmosSelected()
    {
        // �U���͈͂�����
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
