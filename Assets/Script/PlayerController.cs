using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEditor.Tilemaps; // Light2Dを使用するための名前空間

public class PlayerController : MonoBehaviour
{
    //移動
    [SerializeField] float moveSpeed = 5;

    //Light使用
    [SerializeField] Light2D playerLight;
    private bool isUseLight = false;
    private CapsuleCollider2D lightCollider;
    //LightSlider
    [SerializeField] Slider lightSlider;
    [SerializeField] float maxLightTime = 10;
    private float lightTime;

    //ジャンプ
    [SerializeField] float jumpForce = 6;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        //移動
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(x,0,0) * moveSpeed * Time.deltaTime);

        //プレイヤーの向き変更
        if (x != 0) 
            transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);

        //ジャンプ
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        //Light発動
        if (Input.GetKey(KeyCode.L) && lightTime > 0) 
            UseLight();
        else if (Input.GetKeyUp(KeyCode.L) || lightTime <= 0) 
            StopLight();
    }

    //Light発動
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
    //Lightストップ
    private void StopLight()
    {
        if (isUseLight)
        {
            isUseLight = false;
            playerLight.enabled = false;
            lightCollider.enabled = false;
        }
    }

    //ジャンプ判定
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) 
            isGrounded = true;
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
