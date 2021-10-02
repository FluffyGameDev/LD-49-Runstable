using System.Collections;
using UnityEngine;

public class UIUtils
{
    public static IEnumerator ShowUIElement(CanvasGroup canvasGroup, float duration)
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.gameObject.SetActive(true);

        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            float remainingRatio = (endTime - Time.time) / duration;
            canvasGroup.alpha = 1.0f - remainingRatio;
            yield return new WaitForFixedUpdate();
        }

        canvasGroup.alpha = 1.0f;
    }

    public static IEnumerator HideUIElement(CanvasGroup canvasGroup, float duration)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            float remainingRatio = (endTime - Time.time) / duration;
            canvasGroup.alpha = remainingRatio;
            yield return new WaitForFixedUpdate();
        }

        canvasGroup.alpha = 0.0f;
        canvasGroup.gameObject.SetActive(false);
    }
}
