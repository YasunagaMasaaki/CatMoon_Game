using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onplayer : MonoBehaviour
{
    [SerializeField] Transform elevator; // �G���x�[�^�[��Transform
    [SerializeField] GameObject playerOnElevator = null; // �G���x�[�^�[�ɏ���Ă���v���C���[

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerOnElevator == null)
        {
            playerOnElevator = collision.gameObject;
            playerOnElevator.transform.SetParent(elevator); // �q�I�u�W�F�N�g�ɐݒ�
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerOnElevator != null)
            {
                playerOnElevator.transform.SetParent(null, true); // �e�����i���[���h���W�ێ��j
                playerOnElevator = null;
            }
        }
    }
}
