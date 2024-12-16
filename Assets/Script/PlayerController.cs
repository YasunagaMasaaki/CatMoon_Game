using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Light2Dを使用するための名前空間

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;

    [SerializeField] Light2D playerLight;
    [SerializeField] float lightTime = 3f;
    private bool canUseLight = true;
    // ライトの効果範囲のコライダー
    private CapsuleCollider2D lightCollider;

    private Rigidbody2D rb;
    [SerializeField] float jumpForce = 6;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ライトの範囲を表示するためのコライダーを追加
        lightCollider = playerLight.GetComponent<CapsuleCollider2D>();
        if (lightCollider == null)
        {
            lightCollider = playerLight.gameObject.AddComponent<CapsuleCollider2D>();
            lightCollider.isTrigger = true; // トリガーとして設定
        }
        lightCollider.enabled = false; // 最初は無効化
    }

    
    void Update()
    {
        //移動
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(x,0,0) * speed * Time.deltaTime);

        //プレイヤーの向き変更
        if (x != 0) transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);

        //ジャンプ
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        //月の光発動
        if (Input.GetKeyDown(KeyCode.L) && canUseLight) StartCoroutine(MoonLight());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    private IEnumerator MoonLight()
    {
        canUseLight = false;
        playerLight.enabled = true;
        lightCollider.enabled = true;
        yield return new WaitForSeconds(lightTime);
        playerLight.enabled = false;
        lightCollider.enabled = false;
        canUseLight = true;
    }
}
