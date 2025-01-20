using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEditor.Tilemaps; // Light2Dを使用するための名前空間

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    //移動
    [SerializeField] float moveSpeed = 5;

    //Light使用
    [SerializeField] Light2D playerLight;
    private bool isUseLight = false;
    private CapsuleCollider2D lightCollider;
    //LightSlider
    [SerializeField] Slider lightSlider;
    [SerializeField] float maxLightTime = 10f;
    private float lightTime;

    //ジャンプ
    [SerializeField] float jumpForce = 6f;
    [SerializeField] private Rigidbody2D rb;
    private bool isJumping = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        lightTime = maxLightTime;
        lightSlider.maxValue = maxLightTime;
        lightSlider.value = lightTime; 
        

        // ライトの範囲を表示するためのコライダーを追加
        lightCollider = playerLight.GetComponent<CapsuleCollider2D>();
        lightCollider.isTrigger = true; // トリガーとして設定
        lightCollider.enabled = false; // 最初は無効化
    }

    
    void Update()
    {
        Move();
        Jump();

        //Light発動
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
        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);
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

    private void UseLight()
    {
        if (!isUseLight)
        {
            isUseLight = true;
            playerLight.enabled = true;
            lightCollider.enabled = true;
        }

        //使用時間減少
        lightTime -= Time.deltaTime;
        lightTime = Mathf.Clamp(lightTime, 0, maxLightTime);

        //スライダー更新
        if(lightSlider != null) 
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

    //// ライトの時間を回復するメソッド（アイテムなどで使用）
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
