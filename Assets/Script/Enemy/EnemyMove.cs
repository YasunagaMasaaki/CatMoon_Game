using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Animator anim;
    [SerializeField] Vector3[] movePoints;
    [SerializeField] float speed = 3f;

    private int pointIndex = 0;
    private Vector3 previousPosition; // �O�t���[���̈ʒu���L�^

    void Start()
    {
        anim = GetComponent<Animator>();
        previousPosition = transform.position; // �����ʒu��ݒ�
    }

    void Update()
    {
        if (anim.GetBool("Sleep"))
        {
            anim.SetBool("Move", false); // �ړ��A�j���[�V��������~
            return;
        }

        if (movePoints.Length == 0)
            return;

        // ���݂̖ڕW�n�_�Ɍ������Ĉړ�
        transform.position = Vector3.MoveTowards(transform.position, movePoints[pointIndex], speed * Time.deltaTime);

        // ���݂̖ڕW�n�_�ɓ��B�����ꍇ�A���̒n�_�ɐ؂�ւ���
        if (Vector3.Distance(transform.position, movePoints[pointIndex]) < 0.1f)
        {
            pointIndex = (pointIndex + 1) % movePoints.Length; // �C���f�b�N�X�����ɐi�߂�i���[�v������j
        }

        // �ړ��������v�Z
        Vector3 movement = transform.position - previousPosition;
        float x = movement.x; // x�����̈ړ��ʂ��擾

        anim.SetBool("Move", x != 0.0f);

        if (x != 0)
            transform.localScale = new Vector3(Mathf.Sign(-x), 1, 1);

        // ���݂̈ʒu���L�^�i���̃t���[���ňړ��ʂ��v�Z���邽�߁j
        previousPosition = transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < movePoints.Length; i++)
        {
            Gizmos.DrawSphere(movePoints[i], 0.2f); // �ړ��n�_�������ȋ��ŕ`��
        }
    }
}
