using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public Transform targetSign;          // �Ŕ�Transform
    public GameObject textUIObject;       // �\������UI�I�u�W�F�N�g�iCanvas���Text�Ȃǁj

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
