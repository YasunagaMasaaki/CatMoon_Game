using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public GameObject textUIObject;       // �\������UI�I�u�W�F�N�g�iCanvas���Text�Ȃǁj

    void Start()
    {
        textUIObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textUIObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textUIObject.SetActive(false);
        }
    }
}
