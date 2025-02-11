using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEditor.Tilemaps; // Light2D���g�p���邽�߂̖��O���
using UnityEngine.SceneManagement; // �V�[���Ǘ��̂��߂ɕK�v

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [SerializeField,Header("�̗�")]
    private int hp;
    [SerializeField, Header("���G����")]
    private float damageTime;
    [SerializeField,Header("�_�Ŏ���")]
    private float flashTime;
    [SerializeField,Header("�ړ����x")] 
    private float moveSpeed = 5;

    //Light�g�p
    [SerializeField] Light2D playerLight;
    private bool isUseLight = false;
    private CapsuleCollider2D lightCollider;
    //LightSlider
    public Slider lightSlider;
    [SerializeField,Header("���C�g�g�p�\����")]
    public float maxLightTime;
    public float lightTime;

    //�W�����v
    [SerializeField,Header("�W�����v��")] 
    private float jumpForce = 6f;
    [SerializeField] private Rigidbody2D rb;
    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

        lightTime = maxLightTime;
        lightSlider.maxValue = maxLightTime;
        lightSlider.value = lightTime; 
        
        // ���C�g�͈̔͂�\�����邽�߂̃R���C�_�[��ǉ�
        lightCollider = playerLight.GetComponent<CapsuleCollider2D>();
        lightCollider.isTrigger = true; // �g���K�[�Ƃ��Đݒ�
        lightCollider.enabled = false; // �ŏ��͖�����
    }

    void Update()
    {
        Move();
        Jump();

        // �������̃A�j���[�V�����ݒ�
        CheckFall();

        //Light����
        if (Input.GetKey(KeyCode.L) && lightTime > 0) 
            UseLight();
        else if (Input.GetKeyUp(KeyCode.L) || lightTime <= 0) 
            StopLight();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(x, 0, 0) * moveSpeed * Time.deltaTime);
        anim.SetBool("Walk", x != 0.0f);
        // ���C�g�������͌������Œ�
        if (!isUseLight)
        {
            if (x != 0)
                transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stage"))
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }
    }
    void CheckFall()
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // �G�Ƃ̏Փ˂��m�F
        {
            Hit(collision.gameObject); 
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
            StartCoroutine(HitDamage());
        }
    }

    public void Damage(int damage)
    {
        hp = Mathf.Max(hp - damage, 0);
        Dead();
    }

    void Hit(GameObject enemy)
    {
        enemy.GetComponent<AttakSleep>().PlayerDamage(this);
    }

    IEnumerator HitDamage()
    {
        Color color = spriteRenderer.color;
        for(int i =0; i < damageTime; i++)
        {
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b,0.0f); 
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 1.0f);
        }
        spriteRenderer.color = color;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public int GetHP()
    {
        return hp;
    }

    private void Dead()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}

