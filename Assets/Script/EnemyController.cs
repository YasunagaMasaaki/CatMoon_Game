using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isDisabled = false; // ���͉����

    void Start()
    {
        
    }

    void Update()
    {
        if (isDisabled)
        {
            // ���͉����ꂽ��Ԃ̏����i��F�������~�߂�j
            // �����ɖ��͉����̍s����ǉ�
            return;
        }

        // �ʏ�̓G�̓���
        // �����ɓG�̒ʏ�̓�����ǉ�
    }

    // ���͉�����
    public void Disable()
    {
        isDisabled = true;
        // ���͉����̃A�j���[�V��������ʉ��Ȃǂ�ǉ�����ꍇ�͂����Ŏ���
    }

    // ���͉������i�Ⴆ�Έ�莞�Ԍ�Ȃǁj
    public void Enable()
    {
        isDisabled = false;
    }


}
