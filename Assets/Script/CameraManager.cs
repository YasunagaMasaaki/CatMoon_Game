using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField, Header("U“®‚·‚éŠÔ")]
    private float shakeTime;
    [SerializeField, Header("U“®‚Ì‘å‚«‚³")]
    private float shakeMagnitude;

    private PlayerController player;
    private float shakeCount;
    private int currentPlayerHP;

    private Vector3 initPos;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentPlayerHP = player.GetHP();
        initPos = transform.position;
    }

    void Update()
    {
        FollowPlayer();
        ShakeCheck();
    }

    private void ShakeCheck()
    {
        if(currentPlayerHP != player.GetHP())
        {
            currentPlayerHP = player.GetHP();
            shakeCount = 0.0f;
            StartCoroutine(Shake());
        }
    }
    IEnumerator Shake()
    {
        Vector3 initPos = transform.position;

        while(shakeCount < shakeTime)
        {
            float x = initPos.x + Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = initPos.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = new Vector3(x, y,initPos.z);

            shakeCount += Time.deltaTime;

            yield return null;
        }
        transform.position = initPos;
    }

    private void FollowPlayer()
    {
        if (player == null) return;
        float x = player.transform.position.x;
        float y = player.transform.position.y; 
        x = Mathf.Clamp(x,initPos.x,Mathf.Infinity);
        y = Mathf.Clamp(y,initPos.y,Mathf.Infinity);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
