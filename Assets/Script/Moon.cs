using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddMoon(); // �X�R�A��ǉ�

            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.lightTime = playerController.maxLightTime;
            }
            //�X���C�_�[�X�V
            if (playerController.lightSlider != null)
                playerController.lightSlider.value = playerController.lightTime;
            Destroy(gameObject);
        }
    }
}
