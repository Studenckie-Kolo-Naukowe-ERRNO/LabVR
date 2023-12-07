using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenSetting : MonoBehaviour {
    public GridLayoutGroup gridLayoutGroup;
    public float duration;
    public float duration2;
    public Vector2 startingSize;
    public Vector2 startingPos;

    public void OpenTab(GameObject setting) {
        startingPos = setting.GetComponent<RectTransform>().anchoredPosition;
        gridLayoutGroup.enabled = false;
        StartCoroutine(OpenSettingAnimation(setting));
    }

    public void CloseTab(GameObject setting) {
        StartCoroutine(CloseSettingAnimation(setting));
    }

    IEnumerator OpenSettingAnimation(GameObject setting) {
        Vector2 originalSize = setting.GetComponent<RectTransform>().sizeDelta;
        Vector2 targetSize = GetComponent<RectTransform>().sizeDelta;

        float startTime = Time.time;

        while (setting.GetComponent<RectTransform>().sizeDelta != targetSize) {
            float progress = (Time.time - startTime) / duration;

            setting.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(originalSize, targetSize, progress);

            setting.GetComponent<RectTransform>().localPosition = Vector2.Lerp(setting.GetComponent<RectTransform>().localPosition, Vector2.zero, duration2);

            yield return null;
        }

        setting.GetComponent<RectTransform>().localPosition = Vector2.zero;
    }

    IEnumerator CloseSettingAnimation(GameObject setting) {
        Vector2 originalSize = setting.GetComponent<RectTransform>().sizeDelta;
        Vector2 targetSize = startingSize;

        float startTime = Time.time;

        while (setting.GetComponent<RectTransform>().sizeDelta != targetSize) {
            float progress = (Time.time - startTime) / duration;
            float progress2 = (Time.time - startTime) / duration2;

            setting.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(originalSize, targetSize, progress);

            setting.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(setting.GetComponent<RectTransform>().anchoredPosition, startingPos, duration2);

            yield return null;
        }

        setting.GetComponent<RectTransform>().anchoredPosition = startingPos;

        gridLayoutGroup.enabled = true;

    }
}

