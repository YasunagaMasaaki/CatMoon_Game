using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isDisabled = false; // ���͉����
    private SpriteRenderer spriteRenderer; // �G�̃X�v���C�g�����_���[
    private Collider2D enemyCollider; // �G�̃R���C�_�[
    private Rigidbody2D rb; // �G�̃��W�b�h�{�f�B
    private float lightExposureTime = 0f; // ���C�g���������Ă��鎞��
    [SerializeField] float requiredExposureTime = 3f; // ���͉��ɕK�v�Ȏ���

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
            // �����ɖ��͉����̍s����ǉ�
            return;
        }
        // �����ɓG�̒ʏ�̓�����ǉ�
    }

    // ���C�g�������葱�����ꍇ�̏���
    public void OnTriggerStay2D(Collider2D other)
    {
        if (isDisabled) return;

        // �v���C���[�̃��C�g�ɓ������Ă���ꍇ
        if (other.CompareTag("Light"))
        {
            lightExposureTime += Time.deltaTime;

            // ���C�g�������葱���ĕK�v���ԂɒB�����疳�͉�
            if (lightExposureTime >= requiredExposureTime)
            {
                Disable();
            }
        }
    }

    // ���C�g�͈̔͊O�ɏo���ꍇ�̏���
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            lightExposureTime = 0f; // �����葱�������Ԃ����Z�b�g
        }
    }

    // ���͉�����
    public void Disable()
    {
        if (isDisabled) return; // ���łɖ��͉�����Ă���ꍇ�̓X�L�b�v

        isDisabled = true;
        // ���͉����̃A�j���[�V��������ʉ��Ȃǂ�ǉ�����ꍇ�͂����Ŏ���

        if (spriteRenderer != null) spriteRenderer.color = Color.green;
        if (enemyCollider != null) enemyCollider.enabled = false;
        if (rb != null) rb.simulated = false;
    }

    // ���͉������i�Ⴆ�Έ�莞�Ԍ�Ȃǁj
    public void Enable()
    {
        isDisabled = false;

        if (spriteRenderer != null) spriteRenderer.color = Color.white;
        if (enemyCollider != null) enemyCollider.enabled = true;
        if (rb != null) rb.simulated = true;
    }
}
