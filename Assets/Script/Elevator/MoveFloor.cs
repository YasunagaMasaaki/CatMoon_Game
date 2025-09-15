using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveFloor : MonoBehaviour
{
    [SerializeField, Header("�G���x�[�^�[�̏�")]
    private Transform moveFloor;

    [SerializeField, Header("�ړ��n�_")]
    private Transform movePosition;

    [SerializeField, Header("�ړ����x")]
    private float moveSpeed;

    [SerializeField, Header("�X���C�_�[UI")]
    private Slider lightSlider;

    private Vector3 firstPosition;
    private bool isLightHit = false;

    private int lightHitTime = 1;

    private void Start()
    {
        firstPosition = moveFloor ? moveFloor.position : transform.position;

        lightSlider.maxValue = lightHitTime;
        lightSlider.value = 0;
    }

    private void Update()
    {
        if (moveFloor == null || movePosition == null)
            return;

        float speed = Mathf.Max(0, moveSpeed); // ���̒l��h��
        Vector3 targetPosition = isLightHit ? movePosition.position : firstPosition;
        moveFloor.position = Vector3.MoveTowards(moveFloor.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Light"))
        {
            lightSlider.value = lightHitTime;
            SetLightHit(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Light"))
        {
            lightSlider.value = 0;
            SetLightHit(false);
        }
            
    }

    private void SetLightHit(bool value)
    {
        isLightHit = value;
    }
}
