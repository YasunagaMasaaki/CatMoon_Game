using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFadeController : MonoBehaviour
{
    public Animator animator;
    public Image fadeImage;
    public float fadeDuration = 2.0f; // フェード時間

    public Button[] buttons;

    private void Start()
    {
        // アニメーション終了を監視
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        // アニメーションが終了するまで待機
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        // フェードインを開始
        StartCoroutine(FadeInImage());
    }

    private IEnumerator FadeInImage()
    {
        float elapsedTime = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeImage.color = targetColor; // 最終的に完全表示

        // すべてのボタンを表示
        foreach (Button button in buttons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(true);
            }
        }
    }
}
