using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onplayer : MonoBehaviour
{
    [SerializeField] Transform elevator;
    [SerializeField] GameObject playerOnElevator = null; // �G���x�[�^�[�ɏ���Ă���v���C���[

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // �v���C���[���G���x�[�^�[�̎q�I�u�W�F�N�g�ɐݒ�
            playerOnElevator = collision.gameObject;
            playerOnElevator.transform.SetParent(elevator);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // �v���C���[���G���x�[�^�[�̎q�I�u�W�F�N�g�������
            if (playerOnElevator != null)
            {
                playerOnElevator.transform.SetParent(null);
                playerOnElevator = null;
            }
        }
    }
}
