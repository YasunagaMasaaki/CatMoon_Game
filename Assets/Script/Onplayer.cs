using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onplayer : MonoBehaviour
{
    [SerializeField] Transform elevator; // エレベーターのTransform
    [SerializeField] GameObject playerOnElevator = null; // エレベーターに乗っているプレイヤー

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerOnElevator == null)
        {
            playerOnElevator = collision.gameObject;
            playerOnElevator.transform.SetParent(elevator); // 子オブジェクトに設定
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerOnElevator != null)
            {
                playerOnElevator.transform.SetParent(null, true); // 親解除（ワールド座標維持）
                playerOnElevator = null;
            }
        }
    }
}
