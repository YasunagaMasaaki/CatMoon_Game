using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souti : MonoBehaviour
{
    [SerializeField] private float interval = 2f; // 何秒ごとに切り替えるか
    [SerializeField] private bool loop = true;   // 繰り返すかどうか

    private void Start()
    {
        StartCoroutine(ToggleChildrenCoroutine());
    }

    private IEnumerator ToggleChildrenCoroutine()
    {
        // 無限ループ or 一度だけ実行
        do
        {
            foreach (Transform child in transform)
            {
                // 表示する
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(interval);

                // 非表示にする
                child.gameObject.SetActive(false);
                yield return new WaitForSeconds(interval);
            }
        }
        while (loop);
    }
}
