using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField, Header("視野効果"), Range(0, 1)]
    private float parallxEffect;

    [SerializeField, Header("視差効果（Y軸）"), Range(0, 1)]
    private float parallaxEffectY;

    [SerializeField, Header("プレイヤー")]
    private Transform player;

    private GameObject newcamera;
    private float length;
    private float startPosx;
    private float startPosY;

    void Start()
    {
        startPosx = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        newcamera = Camera.main.gameObject;
    }

   
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Parallax();
    }

    private void Parallax()
    {
        float temp = newcamera.transform.position.x * (1- parallxEffect);
        float dist = newcamera.transform.position.x * parallxEffect;

        float distY = (player.position.y - startPosY) * parallaxEffectY;

        transform.position = new Vector3(startPosx + dist, startPosY + distY, transform.position.z);

        if (temp > startPosx + length)
        {
            startPosx += length;
        }
        else if (temp < startPosx - length) 
        {
            startPosx -= length;
        }
    }
}
