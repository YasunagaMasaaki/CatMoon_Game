using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    private float lightHitTime = 0f;
    private float openDoorTime = 2f; //ライトを当てないといけない時間
    [SerializeField, Header("扉が空いてる時間")]
    private float Opening;
    [SerializeField, Header("カウントダウンUI")]
    private Text countdownText;
    [SerializeField, Header("スライダーUI")]
    private Slider lightSlider;
    [SerializeField, Header("カウント音")]
    private GameObject countSE;
    private GameObject currentCountSE; // 生成したカウント音オブジェクトを保持

    void Start()
    {
        countdownText.gameObject.SetActive(false);
        lightSlider.maxValue = openDoorTime;
        lightSlider.value = 0;
    }
    void Update()
    {
        if (lightHitTime > 0)
        {
            lightHitTime += Time.deltaTime;
            lightSlider.value = lightHitTime;

            if (lightHitTime >= openDoorTime)
            {
                OpenDoor();
                lightHitTime = 0f; // リセット
                lightSlider.value = 0; // スライダーもリセット
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
        int remainingTime = Mathf.CeilToInt(Opening); // 小数を切り上げて整数化

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

        countdownText.gameObject.SetActive(false); // カウントダウンを非表示

        if (currentCountSE != null)
        {
            Destroy(currentCountSE);
            currentCountSE = null; // 参照をクリア
        }
    }
}