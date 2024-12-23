using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onplayer : MonoBehaviour
{
    [SerializeField] Transform elevator;
    [SerializeField] GameObject playerOnElevator = null; // エレベーターに乗っているプレイヤー

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // プレイヤーをエレベーターの子オブジェクトに設定
            playerOnElevator = collision.gameObject;
            playerOnElevator.transform.SetParent(elevator);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // プレイヤーをエレベーターの子オブジェクトから解除
            if (playerOnElevator != null)
            {
                playerOnElevator.transform.SetParent(null);
                playerOnElevator = null;
            }
        }
    }
}
