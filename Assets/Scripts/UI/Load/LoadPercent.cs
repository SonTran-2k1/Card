using System.Collections;
using TMPro;
using UnityEngine;

public class LoadPercent : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public float jumpDuration = 3f;

    private void Start()
    {
        StartCoroutine(AnimateTextJump());
    }

    IEnumerator AnimateTextJump()
    {
        float timer = 0f;

        while (timer < jumpDuration)
        {
            float progress = timer / jumpDuration;
            int percentage = Mathf.RoundToInt(progress * 100);

            textMeshPro.text = percentage + "%";

            timer += Time.deltaTime;
            yield return null;
        }

        // Set text to 100% after animation completes
        textMeshPro.text = "100%";

        // Activate object A
        UiManager.Instance._uiController._btnStartGame.gameObject.SetActive(true);
    }
}
