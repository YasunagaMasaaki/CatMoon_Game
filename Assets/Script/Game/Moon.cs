using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField, Header("�Q�b�g��")]
    private GameObject getSE;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddMoon(); // �X�R�A��ǉ�

            Instantiate(getSE);

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
