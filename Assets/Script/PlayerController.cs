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
        //移動
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(x,0,0) * speed * Time.deltaTime);

        //月の光発動
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
        // ここで再び使用可能にする場合は特定の条件をチェック
    }
}
