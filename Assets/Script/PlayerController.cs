using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEditor.Tilemaps; // Light2D���g�p���邽�߂̖��O���

public class PlayerController : MonoBehaviour
{
    //�ړ�
    [SerializeField] float moveSpeed = 5;

    private Animator anim;

    //Light�g�p
    [SerializeField] Light2D playerLight;
    private bool isUseLight = false;
    private CapsuleCollider2D lightCollider;
    //LightSlider
    [SerializeField] Slider lightSlider;
    [SerializeField] float maxLightTime = 10;
    private float lightTime;

    //�W�����v
    [SerializeField] float jumpForce = 6;
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] Collider2D groundCheckCollider; // �����p�̃g���K�[�R���C�_�[

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        lightTime = maxLightTime;
        lightSlider.maxValue = maxLightTime;
        lightSlider.value = lightTime; 
        

        // ���C�g�͈̔͂�\�����邽�߂̃R���C�_�[��ǉ�
        lightCollider = playerLight.GetComponent<CapsuleCollider2D>();
        lightCollider.isTrigger = true; // �g���K�[�Ƃ��Đݒ�
        lightCollider.enabled = false; // �ŏ��͖�����

        // ���C�g�̃R���C�_�[���n�ʂƊ����Ȃ��悤Layer��ݒ�
        //Physics2D.IgnoreCollision(lightCollider, groundCheckCollider, true);
    }

    
    void Update()
    {
        //�ړ�
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(x,0,0) * moveSpeed * Time.deltaTime);
        anim.SetBool("Walk",x != 0.0f);

        //�v���C���[�̌����ύX
        if (x != 0) 
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);

        //�W�����v
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("Jump", true);
        }

        //Light����
        if (Input.GetKey(KeyCode.L) && lightTime > 0) 
            UseLight();
        else if (Input.GetKeyUp(KeyCode.L) || lightTime <= 0) 
            StopLight();
    }

    //Light����
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
        if(lightSlider != null) 
            lightSlider.value = lightTime;
    }
    //Light�X�g�b�v
    private void StopLight()
    {
        if (isUseLight)
        {
            isUseLight = false;
            playerLight.enabled = false;
            lightCollider.enabled = false;
        }
    }

    // ������p�R���C�_�[�ł̐ڒn����
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && other == groundCheckCollider)
        {
            isGrounded = true;
            anim.SetBool("Jump", false);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && other == groundCheckCollider)
        {
            isGrounded = false;
        }
    }

    //// ���C�g�̎��Ԃ��񕜂��郁�\�b�h�i�A�C�e���ȂǂŎg�p�j
    //public void RechargeLight(float amount)
    //{
    //    currentLightTime += amount;
    //    currentLightTime = Mathf.Clamp(currentLightTime, 0, maxLightTime);

    //    if (lightSlider != null)
    //    {
    //        lightSlider.value = currentLightTime;
    //    }
    //}
}
