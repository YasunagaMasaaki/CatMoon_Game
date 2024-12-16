using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Light2D‚ðŽg—p‚·‚é‚½‚ß‚Ì–¼‘O‹óŠÔ

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] Light2D playerLight;
    [SerializeField] float lightTime = 3f;
    private bool canUseLight = true;

    void Start()
    {
        
    }

    
    void Update()
    {
        //ˆÚ“®
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(x,0,0) * speed * Time.deltaTime);

        //ŒŽ‚ÌŒõ”­“®
        if(Input.GetKeyDown(KeyCode.L) && canUseLight) StartCoroutine(MoonLight());
    }

    private IEnumerator MoonLight()
    {
        canUseLight = false;
        playerLight.enabled = true;
        yield return new WaitForSeconds(lightTime);
        playerLight.enabled = false;
        canUseLight = true;
    }
}
