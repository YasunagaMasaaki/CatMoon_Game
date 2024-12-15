using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;

    [SerializeField] Light playerLight;
    [SerializeField] float lightTime = 3f;
    private bool canUseLight = true;

    void Start()
    {
        
    }

    
    void Update()
    {
        //�ړ�
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(x,0,0) * speed * Time.deltaTime);

        //���̌�����
        if(Input.GetKeyDown(KeyCode.L) && canUseLight)
        {
            StartCoroutine(MoonLight());
        }
    }

    private IEnumerator MoonLight()
    {
        canUseLight = false;
        playerLight.enabled = true;
        yield return new WaitForSeconds(lightTime);
        playerLight.enabled = false;
        // �����ōĂюg�p�\�ɂ���ꍇ�͓���̏������`�F�b�N
    }
}
