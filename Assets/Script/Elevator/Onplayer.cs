using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onplayer : MonoBehaviour
{
    [SerializeField,Header("�G���x�[�^�[��Transform")]
    private Transform elevator;
    [SerializeField, Header("�G���x�[�^�[�ɏ���Ă���v���C���[")]
    private GameObject playerOnElevator = null;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerOnElevator == null)
        {
            playerOnElevator = collision.gameObject;
            playerOnElevator.transform.SetParent(elevator); // �G���x�[�^�[��e�ɐݒ�
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerOnElevator != null)
            {
                playerOnElevator.transform.SetParent(null); // �e�q�֌W������
                playerOnElevator = null;
            }
        }
    }
}
