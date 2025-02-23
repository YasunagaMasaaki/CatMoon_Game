using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFadeController : MonoBehaviour
{
    public Animator animator;  // �A�j���[�^�[
    public Image fadeImage;    // �t�F�[�h�C��������UI�摜
    public float fadeDuration = 2.0f; // �t�F�[�h����

    public Button[] buttons;    // �t�F�[�h��ɕ\������{�^���̔z��

    private void Start()
    {
        // �A�j���[�V�����I�����Ď�
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        // �A�j���[�V�������I������܂őҋ@
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        // �t�F�[�h�C�����J�n
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

        fadeImage.color = targetColor; // �ŏI�I�Ɋ��S�\��

        // ���ׂẴ{�^����\��
        foreach (Button button in buttons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(true);
            }
        }
    }
}
