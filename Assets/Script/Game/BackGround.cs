using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField, Header("視野効果"), Range(0, 1)]
    private float parallxEffect;

    private GameObject newcamera;
    private float length;
    private float startPosx;

    private Vector3 initPos;

    void Start()
    {
        startPosx = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x * 2;
        newcamera = Camera.main.gameObject;

        initPos = transform.position;
    }

    void Update()
    {
        FollowCamera();
    }

    private void FixedUpdate()
    {
        Parallax();
    }

    //上下カメラ追従
    private void FollowCamera()
    {
        float y = newcamera.transform.position.y - 7f;
        y = Mathf.Clamp(y, initPos.y, Mathf.Infinity);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    //左右カメラ追従
    private void Parallax()
    {
        float temp = newcamera.transform.position.x * (1- parallxEffect);
        float dist = newcamera.transform.position.x * parallxEffect;

        transform.position = new Vector3(startPosx + dist, transform.position.y, transform.position.z);

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
