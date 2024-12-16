using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isDisabled = false; // 無力化状態

    void Start()
    {
        
    }

    void Update()
    {
        if (isDisabled)
        {
            // 無力化された状態の処理（例：動きを止める）
            // ここに無力化中の行動を追加
            return;
        }

        // 通常の敵の動き
        // ここに敵の通常の動きを追加
    }

    // 無力化処理
    public void Disable()
    {
        isDisabled = true;
        // 無力化時のアニメーションや効果音などを追加する場合はここで実装
    }

    // 無力化解除（例えば一定時間後など）
    public void Enable()
    {
        isDisabled = false;
    }


}
