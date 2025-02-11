using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEditor.Tilemaps; // Light2Dを使用するための名前空間
using UnityEngine.SceneManagement; // シーン管理のために必要

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [SerializeField,Header("体力")]
    private int hp;
    [SerializeField, Header("無敵時間")]
    private float damageTime;
    [SerializeField,Header("点滅時間")]
    private float flashTime;
    [SerializeField,Header("移動速度")] 
    private float moveSpeed = 5;

    //Light使用
    [SerializeField] Light2D playerLight;
    private bool isUseLight = false;
    private CapsuleCollider2D lightCollider;
    //LightSlider
    public Slider lightSlider;
    [SerializeField,Header("ライト使用可能時間")]
    public float maxLightTime;
    public float lightTime;

    //ジャンプ
    [SerializeField,Header("ジャンプ力")] 
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
        
        // ライトの範囲を表示するためのコライダーを追加
        lightCollider = playerLight.GetComponent<CapsuleCollider2D>();
        lightCollider.isTrigger = true; // トリガーとして設定
        lightCollider.enabled = false; // 最初は無効化
    }

    void Update()
    {
        Move();
        Jump();

        // 落下中のアニメーション設定
        CheckFall();

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
        // ライト発動中は向きを固定
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

        //使用時間減少
        lightTime -= Time.deltaTime;
        lightTime = Mathf.Clamp(lightTime, 0, maxLightTime);

        //スライダー更新
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
        if (collision.gameObject.CompareTag("Enemy")) // 敵との衝突を確認
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

