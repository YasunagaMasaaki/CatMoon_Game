using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private PlayerController player;
    private Vector3 initPos;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        initPos = transform.position;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null) return;
        float x = player.transform.position.x;
        float y = player.transform.position.y; 
        x = Mathf.Clamp(x,initPos.x,Mathf.Infinity);
        y = Mathf.Clamp(y,initPos.y,Mathf.Infinity);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
