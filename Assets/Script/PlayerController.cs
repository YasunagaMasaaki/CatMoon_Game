using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Light2D���g�p���邽�߂̖��O���

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;

    [SerializeField] Light2D playerLight;
    [SerializeField] float lightTime = 3f;
    private bool canUseLight = true;
    // ���C�g�̌��ʔ͈͂̃R���C�_�[
    private CapsuleCollider2D lightCollider;

    private Rigidbody2D rb;
    [SerializeField] float jumpForce = 6;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ���C�g�͈̔͂�\�����邽�߂̃R���C�_�[��ǉ�
        lightCollider = playerLight.GetComponent<CapsuleCollider2D>();
        if (lightCollider == null)
        {
            lightCollider = playerLight.gameObject.AddComponent<CapsuleCollider2D>();
            lightCollider.isTrigger = true; // �g���K�[�Ƃ��Đݒ�
        }
        lightCollider.enabled = false; // �ŏ��͖�����
    }

    
    void Update()
    {
        //�ړ�
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(x,0,0) * speed * Time.deltaTime);

        //�v���C���[�̌����ύX
        if (x != 0) transform.localScale = new Vector3(Mathf.Sign(x), 1, 1);

        //�W�����v
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        //���̌�����
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
