using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField, Header("Ž‹–ìŒø‰Ê"), Range(0, 1)]
    private float parallxEffect;

    private GameObject newcamera;
    private float length;
    private float startPosx;

    private float minY;
    //private Vector3 initPos;

    void Start()
    {
        startPosx = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        newcamera = Camera.main.gameObject;


        minY = transform.position.y;
    }

   
    void Update()
    {
        //FollowPlayer();
    }

    private void FixedUpdate()
    {
        Parallax();
    }

    //private void FollowPlayer()
    //{
        
    //    float y = newcamera.transform.position.y;
    //    y = Mathf.Clamp(y, minY, Mathf.Infinity);
    //    transform.position = new Vector3(transform.position.x, y, transform.position.z);
    //}

    private void Parallax()
    {
        float temp = newcamera.transform.position.x * (1- parallxEffect);
        float dist = newcamera.transform.position.x * parallxEffect;

        float y = newcamera.transform.position.y;
        y = Mathf.Clamp(y, minY, Mathf.Infinity);
    

        transform.position = new Vector3(startPosx + dist,y, transform.position.z);

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
