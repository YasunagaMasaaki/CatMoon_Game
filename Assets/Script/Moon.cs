using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �v���C���[�ƐڐG�����ꍇ
        {
            GameManager.instance.AddMoon(); // �X�R�A��ǉ�
            Destroy(gameObject); // �I�u�W�F�N�g���폜
        }
    }
}
