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

    [SerializeField,Header("体力")]
    private int hp;
    [SerializeField, Header("移動速度")]
    private float moveSpeed;
    [SerializeField, Header("ジャンプ力")]
    private float jumpForce;
    private bool isJumping = false;

    [SerializeField, Header("ライト使用可能時間")]
    public float maxLightTime;
    [SerializeField, Header("ライト本体")] 
    private Light2D playerLight;
    private CapsuleCollider2D lightCollider;
    private bool isUseLight = false;
    public Slider lightSlider;
    public float lightTime;

    [SerializeField, Header("無敵時間")]
    private float damageTime;
    [SerializeField, Header("点滅時間")]
    private float flashTime;
    [SerializeField,Header("ノックバックの強さ")] 
    private float knockbackForce;
    private float knockbackDuration = 0.2f;
    private bool isKnockedBack = false;

    [SerializeField, Header("ジャンプ音")]
    private GameObject jumpSE; 
    [SerializeField, Header("ダメージ音")]
    private GameObject damageSE;
    [SerializeField, Header("ライト音")]
    private GameObject lightSE;
    private GameObject currentLightSE; // 生成したSEオブジェクトを保持

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        lightTime = maxLightTime;
        lightSlider.maxValue = maxLightTime;
        lightSlider.value = lightTime;
        // ライトの範囲を表示するためのコライダーを追加
        lightCollider = playerLight.GetComponent<CapsuleCollider2D>();
        lightCollider.isTrigger = true;
        lightCollider.enabled = false; // 最初は無効化
    }

    void Update()
    {
        Move();
        Jump();

        // 落下中のアニメーション設定
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
        // ライト発動中は向きを固定
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
    //無限ジャンプ阻止
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

            if (currentLightSE != null)
            {
                Destroy(currentLightSE);
                currentLightSE = null; // 参照をクリア
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

