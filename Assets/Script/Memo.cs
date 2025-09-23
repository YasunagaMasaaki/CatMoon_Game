using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Memo : MonoBehaviour
{
    [SerializeField] private GameObject memoText;

    [SerializeField, Header("ページ音")]
    private GameObject nextPageSE;
    [SerializeField, Header("閉じる音")]
    private GameObject closeSE;

    private bool hasPlayed = false; // 一度だけ生成するためのフラグ

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            Instantiate(nextPageSE);
            memoText.SetActive(true);
            hasPlayed = true; // もう生成したのでフラグを立てる
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
