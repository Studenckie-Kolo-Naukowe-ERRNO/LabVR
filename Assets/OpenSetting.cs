using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpenSetting : MonoBehaviour {
    [SerializeField] private float resizeIconSpeed;
    [SerializeField] private float centreIconSpeed;
    private GridLayoutGroup gridLayoutGroup;
    private Vector2 startingSize;
    private Vector2 startingPos;
    private Vector2 openedTabSize;
    private GameObject lastOpenedTab;

    private void Start() {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        openedTabSize = GetComponent<RectTransform>().sizeDelta;
    }

    public void OpenTab(GameObject iconGameObject) {
        lastOpenedTab = iconGameObject;
        startingPos = iconGameObject.GetComponent<RectTransform>().localPosition;
        startingSize = iconGameObject.GetComponent<RectTransform>().sizeDelta;
        iconGameObject.GetComponent<Button>().enabled = false;

        gridLayoutGroup.enabled = false;
        StartCoroutine(ToggleSettingAnimation(
            iconGameObject, 
            openedTabSize, 
            Vector2.zero, 
            false, 
            false,
            true
        ));
        
        for (int i = 0; i < transform.childCount - 1; i++) {
            if (transform.GetChild(i).gameObject != lastOpenedTab) {
                transform.GetChild(i).gameObject.gameObject.SetActive(false);
            }
        }
    }

    public void CloseTab() {

        foreach (Transform settings in lastOpenedTab.transform) {
            settings.gameObject.SetActive(false);
        }

        foreach (Transform icon in transform) {
            icon.gameObject.SetActive(true);
        }

        StartCoroutine(ToggleSettingAnimation(
            lastOpenedTab, 
            startingSize, 
            startingPos, 
            true, 
            true,
            false
        ));

    }

    IEnumerator ToggleSettingAnimation(GameObject iconGameObject, 
        Vector2 finalSize, Vector2 finalPos, 
        bool gridEnabled, bool btnEnabled, bool settingsEnabled) {

        Vector2 originalSize = iconGameObject.GetComponent<RectTransform>().sizeDelta;
        RectTransform rectTransform = iconGameObject.GetComponent<RectTransform>();

        float startTime = Time.time;

        while (rectTransform.sizeDelta != finalSize) {
            float progress = (Time.time - startTime) / resizeIconSpeed;

            rectTransform.sizeDelta = Vector2.Lerp(
                originalSize, 
                finalSize, 
                progress
            );

            rectTransform.localPosition = Vector2.Lerp(
                rectTransform.localPosition, 
                finalPos, 
                centreIconSpeed
            );

            yield return null;
        }

        rectTransform.localPosition = finalPos;
        gridLayoutGroup.enabled = gridEnabled;
        iconGameObject.GetComponent<Button>().enabled = btnEnabled;

        foreach (Transform settings in iconGameObject.transform) {
            settings.gameObject.SetActive(settingsEnabled);
        }
    }

}

