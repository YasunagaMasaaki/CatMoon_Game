using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                // �v���C���[�̃��X�|�[���n�_���X�V
                player.SetCheckpoint(transform.position);
                Debug.Log("�`�F�b�N�|�C���g�X�V�I");
            }
        }
    }
}
