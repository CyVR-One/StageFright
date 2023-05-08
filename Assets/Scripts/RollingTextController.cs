using System.Collections;
using UnityEngine;
using TMPro;

public class RollingTextController : MonoBehaviour
{
    public float rollingSpeed = 1.0f;
    private TextMeshProUGUI rollingText;

    void Start()
    {
        rollingText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(RollText());
    }

    IEnumerator RollText()
    {
        while (true)
        {
            float newYPosition = rollingText.rectTransform.anchoredPosition.y + rollingSpeed * Time.deltaTime;
            rollingText.rectTransform.anchoredPosition = new Vector2(rollingText.rectTransform.anchoredPosition.x, newYPosition);
            yield return null;
        }
    }
}
