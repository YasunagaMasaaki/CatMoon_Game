using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onplayer : MonoBehaviour
{
    [SerializeField,Header("エレベーターのTransform")]
    private Transform elevator;
    [SerializeField, Header("エレベーターに乗っているプレイヤー")]
    private GameObject playerOnElevator = null;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerOnElevator == null)
        {
            playerOnElevator = collision.gameObject;
            playerOnElevator.transform.SetParent(elevator); // エレベーターを親に設定
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerOnElevator != null)
            {
                playerOnElevator.transform.SetParent(null); // 親子関係を解除
                playerOnElevator = null;
            }
        }
    }
}
