using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField, Header("ゲット音")]
    private GameObject getSE;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddMoon(); // スコアを追加

            Instantiate(getSE);

            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.lightTime = playerController.maxLightTime;
            }
            //スライダー更新
            if (playerController.lightSlider != null)
                playerController.lightSlider.value = playerController.lightTime;
            Destroy(gameObject);
        }
    }
}
