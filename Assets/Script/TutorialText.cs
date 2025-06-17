using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public Transform targetSign;          // 看板のTransform
    public GameObject textUIObject;       // 表示するUIオブジェクト（Canvas上のTextなど）

    private bool isShowing = false;

    void Start()
    {
        textUIObject.SetActive(false);
    }

    void Update()
    {
        if (isShowing)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(targetSign.position);
            textUIObject.transform.position = screenPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isShowing = true;
            textUIObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isShowing = false;
            textUIObject.SetActive(false);
        }
    }
}
