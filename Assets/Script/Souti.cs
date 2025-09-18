using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souti : MonoBehaviour
{
    [SerializeField] private float interval = 2f; // ���b���Ƃɐ؂�ւ��邩
    [SerializeField] private bool loop = true;   // �J��Ԃ����ǂ���

    private void Start()
    {
        StartCoroutine(ToggleChildrenCoroutine());
    }

    private IEnumerator ToggleChildrenCoroutine()
    {
        // �������[�v or ��x�������s
        do
        {
            foreach (Transform child in transform)
            {
                // �\������
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(interval);

                // ��\���ɂ���
                child.gameObject.SetActive(false);
                yield return new WaitForSeconds(interval);
            }
        }
        while (loop);
    }
}
