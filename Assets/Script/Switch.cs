using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    private float lightHitTime = 0f;
    private float openDoorTime = 2f;
    [SerializeField, Header("扉が空いてる時間")]
    private float Opening;
    [SerializeField, Header("カウントダウンUI")]
    private Text countdownText; // UIのText (TextMeshProの場合は TMP_Text)

    void Start()
    {
        countdownText.gameObject.SetActive(false); // 最初は非表示
    }

    // Update is called once per frame
    void Update()
    {
        if (lightHitTime > 0)
        {
            lightHitTime += Time.deltaTime;

            if (lightHitTime >= openDoorTime)
            {
                OpenDoor();
                return;
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
            lightHitTime = 0f;
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
    }
}
