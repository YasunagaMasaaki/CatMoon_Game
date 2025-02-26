using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField, Header("�G���x�[�^�[�̏�")]
    private Transform moveFloor;

    [SerializeField, Header("�ړ��n�_")]
    private Transform movePosition;

    [SerializeField, Header("�ړ����x")]
    private float moveSpeed;

    private Vector3 firstPosition;
    private bool isLightHit = false;

    private void Start()
    {
        firstPosition = moveFloor ? moveFloor.position : transform.position;
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
            SetLightHit(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Light"))
            SetLightHit(false);
    }

    private void SetLightHit(bool value)
    {
        isLightHit = value;
    }
}
