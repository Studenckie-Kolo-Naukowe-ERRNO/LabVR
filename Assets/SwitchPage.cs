using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchPage : MonoBehaviour {
    [SerializeField] private GameObject[] pages;
    [SerializeField] private GameObject arrowLeft;
    [SerializeField] private GameObject arrowRight;
    private int currentPage = 0;
    private Vector2 nextPageStartingPosRight;
    private Vector2 nextPageStartingPosLeft;

    private void Start() {
        nextPageStartingPosRight = new Vector2(GetComponent<RectTransform>().sizeDelta.x, 0);
        nextPageStartingPosLeft = new Vector2(-GetComponent<RectTransform>().sizeDelta.x, 0);

        if (pages.Length == 0 || pages.Length == 1) {
            Debug.LogWarning("No pages added");
            arrowLeft.SetActive(false);
            arrowRight.SetActive(false);
        }
    }

    private bool ShouldContinueMoving(int direction) {
        RectTransform currentPageRect = pages[currentPage].GetComponent<RectTransform>();
        return (currentPageRect.anchoredPosition.x >= 0 && direction == -1) ||
               (currentPageRect.anchoredPosition.x <= 0 && direction == 1);
    }

    public void SwitchRight() {
        currentPage++;
        if (currentPage >= pages.Length) currentPage = 0;
        pages[currentPage].GetComponent<RectTransform>().anchoredPosition = nextPageStartingPosRight;

        StartCoroutine(SwapPage(-1));
        pages[currentPage].SetActive(true);
    }       
    
    public void SwitchLeft() {
        currentPage--;
        if (currentPage < 0) currentPage = pages.Length-1;
        pages[currentPage].GetComponent<RectTransform>().anchoredPosition = nextPageStartingPosLeft;

        StartCoroutine(SwapPage(1));
        pages[currentPage].SetActive(true);
    }

    IEnumerator SwapPage(int direction) {
        Vector2 pagePos = pages[currentPage].GetComponent<RectTransform>().anchoredPosition;

        while (ShouldContinueMoving(direction)) {
            yield return new WaitForSeconds(0.01f);
            foreach (GameObject page in pages) {
                page.transform.position += new Vector3(direction * 10, 0, 0);
            }
        }

        pages[currentPage].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}

