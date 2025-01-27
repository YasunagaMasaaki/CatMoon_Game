using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Animator anim;
    [SerializeField] Vector3[] movePoints;
    [SerializeField] float speed = 3f;

    private int pointIndex = 0;
    private Vector3 previousPosition; // 前フレームの位置を記録

    void Start()
    {
        anim = GetComponent<Animator>();
        previousPosition = transform.position; // 初期位置を設定
    }

    void Update()
    {
        if (anim.GetBool("Sleep"))
        {
            anim.SetBool("Move", false); // 移動アニメーションも停止
            return;
        }

        if (movePoints.Length == 0)
            return;

        // 現在の目標地点に向かって移動
        transform.position = Vector3.MoveTowards(transform.position, movePoints[pointIndex], speed * Time.deltaTime);

        // 現在の目標地点に到達した場合、次の地点に切り替える
        if (Vector3.Distance(transform.position, movePoints[pointIndex]) < 0.1f)
        {
            pointIndex = (pointIndex + 1) % movePoints.Length; // インデックスを次に進める（ループさせる）
        }

        // 移動方向を計算
        Vector3 movement = transform.position - previousPosition;
        float x = movement.x; // x方向の移動量を取得

        anim.SetBool("Move", x != 0.0f);

        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(-x), 1, 1);

        // 現在の位置を記録（次のフレームで移動量を計算するため）
        previousPosition = transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < movePoints.Length; i++)
        {
            Gizmos.DrawSphere(movePoints[i], 0.2f); // 移動地点を小さな球で描画
        }
    }
}
