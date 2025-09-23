using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Memo : MonoBehaviour
{
    [SerializeField] private GameObject memoText;

    [SerializeField, Header("�y�[�W��")]
    private GameObject nextPageSE;
    [SerializeField, Header("���鉹")]
    private GameObject closeSE;

    private bool hasPlayed = false; // ��x�����������邽�߂̃t���O

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            Instantiate(nextPageSE);
            memoText.SetActive(true);
            hasPlayed = true; // �������������̂Ńt���O�𗧂Ă�
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hasPlayed)
        {
            Instantiate(closeSE);
            memoText.SetActive(false);
            hasPlayed = false;
        }
    }
}
