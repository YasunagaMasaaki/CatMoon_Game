using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    private float lightHitTime = 0f;
    private float openDoorTime = 2f;
    [SerializeField, Header("�����󂢂Ă鎞��")]
    private float Opening;
    [SerializeField, Header("�J�E���g�_�E��UI")]
    private Text countdownText; // UI��Text (TextMeshPro�̏ꍇ�� TMP_Text)

    [SerializeField, Header("�X���C�_�[UI")]
    private Slider lightSlider; // �X�C�b�`�̃X���C�_�[

    [SerializeField, Header("�J�E���g��")]
    private GameObject countSE;

    private GameObject currentCountSE; // ���������J�E���g���I�u�W�F�N�g��ێ�

    void Start()
    {
        countdownText.gameObject.SetActive(false); // �ŏ��͔�\��

        lightSlider.maxValue = openDoorTime;
        lightSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightHitTime > 0)
        {
            lightHitTime += Time.deltaTime;
            lightSlider.value = lightHitTime; // �X���C�_�[�X�V

            if (lightHitTime >= openDoorTime)
            {
                OpenDoor();
                lightHitTime = 0f; // ���Z�b�g
                lightSlider.value = 0; // �X���C�_�[�����Z�b�g
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
            lightHitTime = 0.01f;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            lightHitTime = 0f;
            lightSlider.value = 0;
        }

    }

    private void OpenDoor()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CloseDoorAfterTime(doors));
    }

    private IEnumerator CloseDoorAfterTime(GameObject[] doors)
    {
        int remainingTime = Mathf.CeilToInt(Opening); // ������؂�グ�Đ�����

        if (currentCountSE == null && countSE != null)
        {
            currentCountSE = Instantiate(countSE);
        }

        while (remainingTime > 0)
        {
            countdownText.text = $"{remainingTime}";
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }

        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }

        countdownText.gameObject.SetActive(false); // �J�E���g�_�E�����\��

        if (currentCountSE != null)
        {
            Destroy(currentCountSE);
            currentCountSE = null; // �Q�Ƃ��N���A
        }
    }
}