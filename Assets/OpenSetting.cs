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
    private GameObject lastOpenedTab;

    public void OpenTab(GameObject setting) {
        lastOpenedTab = setting;
        startingPos = setting.GetComponent<RectTransform>().anchoredPosition;
        gridLayoutGroup.enabled = false;
        StartCoroutine(OpenSettingAnimation(setting));
    }

    public void CloseTab() {
        StartCoroutine(CloseSettingAnimation());
        //lastOpenedTab = null;
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

    IEnumerator CloseSettingAnimation() {
        Vector2 originalSize = lastOpenedTab.GetComponent<RectTransform>().sizeDelta;
        Vector2 targetSize = startingSize;

        float startTime = Time.time;

        while (lastOpenedTab.GetComponent<RectTransform>().sizeDelta != targetSize) {
            float progress = (Time.time - startTime) / duration;
            float progress2 = (Time.time - startTime) / duration2;

            lastOpenedTab.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(originalSize, targetSize, progress);

            lastOpenedTab.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(lastOpenedTab.GetComponent<RectTransform>().anchoredPosition, startingPos, duration2);

            yield return null;
        }

        lastOpenedTab.GetComponent<RectTransform>().anchoredPosition = startingPos;

        gridLayoutGroup.enabled = true;

    }
}

