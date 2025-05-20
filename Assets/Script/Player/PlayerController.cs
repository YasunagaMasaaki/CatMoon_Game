using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField,Header("�̗�")]
    private int hp;
    [SerializeField, Header("�ړ����x")]
    private float moveSpeed;
    [SerializeField, Header("�W�����v��")]
    private float jumpForce;
    private bool isJumping = false;

    [SerializeField, Header("���C�g�g�p�\����")]
    public float maxLightTime;
    [SerializeField, Header("���C�g�{��")] 
    private Light2D playerLight;
    private CapsuleCollider2D lightCollider;
    private bool isUseLight = false;
    public Slider lightSlider;
    public float lightTime;

    [SerializeField, Header("���G����")]
    private float damageTime;
    [SerializeField, Header("�_�Ŏ���")]
    private float flashTime;
    [SerializeField,Header("�m�b�N�o�b�N�̋���")] 
    private float knockbackForce;
    private float knockbackDuration = 0.2f;
    private bool isKnockedBack = false;

    [SerializeField, Header("�W�����v��")]
    private GameObject jumpSE; 
    [SerializeField, Header("�_���[�W��")]
    private GameObject damageSE;
    [SerializeField, Header("���C�g��")]
    private GameObject lightSE;
    private GameObject currentLightSE; // ��������SE�I�u�W�F�N�g��ێ�

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        lightTime = maxLightTime;
        lightSlider.maxValue = maxLightTime;
        lightSlider.value = lightTime;
        // ���C�g�͈̔͂�\�����邽�߂̃R���C�_�[��ǉ�
        lightCollider = playerLight.GetComponent<CapsuleCollider2D>();
        lightCollider.isTrigger = true;
        lightCollider.enabled = false; // �ŏ��͖�����
    }

    void Update()
    {
        Move();
        Jump();

        // �������̃A�j���[�V�����ݒ�
        CheckFall();

        float trigger = Input.GetAxis("Triggers");

        bool isLightButtonDown = Input.GetMouseButton(0) || trigger > 0.1f;
        bool isLightButtonUp = Input.GetMouseButtonUp(0) || trigger <= 0.1f;

        if (isLightButtonDown && lightTime > 0)
            UseLight();
        else if (isLightButtonUp || lightTime <= 0)
            StopLight();

        if (!isUseLight)
        {
            RecoveryLight();
        }
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(x, 0, 0) * moveSpeed * Time.deltaTime);
        anim.SetBool("Walk", Mathf.Abs(x) > 0.01f);
        // ���C�g�������͌������Œ�
        if (!isUseLight)
        {
            if (x != 0)
                transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Instantiate(jumpSE);
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
        }
    }
    //�����W�����v�j�~
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stage"))
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }
    }
    private void CheckFall()
    {
        if (rb.velocity.y < -1)
            anim.SetBool("Fall", true);  
        else
            anim.SetBool("Fall", false); 
    }

    private void UseLight()
    {
        if (!isUseLight)
        {
            isUseLight = true;
            playerLight.enabled = true;
            lightCollider.enabled = true;

            if (currentLightSE == null)
                currentLightSE = Instantiate(lightSE);
        }
        //�g�p���Ԍ���
        lightTime -= Time.deltaTime;
        lightTime = Mathf.Clamp(lightTime, 0, maxLightTime);
        //�X���C�_�[�X�V
        if (lightSlider != null)
            lightSlider.value = lightTime;
    }
    private void StopLight()
    {
        if (isUseLight)
        {
            isUseLight = false;
            playerLight.enabled = false;
            lightCollider.enabled = false;

            if (currentLightSE != null)
            {
                Destroy(currentLightSE);
                currentLightSE = null; // �Q�Ƃ��N���A
            }
        }
    }

    private void RecoveryLight()
    {
        if (lightTime < maxLightTime)
            lightTime = Mathf.Min(lightTime + Time.deltaTime*0.4f, maxLightTime);
        if (lightSlider != null)
            lightSlider.value = lightTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        Instantiate(damageSE);
        Hit(collision.gameObject);
        StartCoroutine(Muteki());
    }

    public void Damage(int damage, Vector2 knockbackDir)
    {
        hp = Mathf.Max(hp - damage, 0);
        if (hp <= 0) Destroy(gameObject);
        else if (!isKnockedBack) StartCoroutine(Knockback(knockbackDir));
    }

    private void Hit(GameObject enemy)
    {
        Vector2 knockbackDir = (transform.position - enemy.transform.position).normalized;
        enemy.GetComponent<AttakSleep>()?.PlayerDamage(this, knockbackDir);
    }

    private IEnumerator Knockback(Vector2 direction)
    {
        isKnockedBack = true;
        rb.velocity = direction * knockbackForce;
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    private IEnumerator Muteki()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public int GetHP() => hp;
}

