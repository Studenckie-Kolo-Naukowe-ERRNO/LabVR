using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchPage : MonoBehaviour {
    [SerializeField] private GameObject[] pages;
    private int currentPage = 0;
    private Vector2 nextPageStartingPosRight;
    private Vector2 nextPageStartingPosLeft;
    [SerializeField] private GameObject arrowLeft;
    [SerializeField] private GameObject arrowRight;

    private void Start() {
        nextPageStartingPosRight = new Vector2(GetComponent<RectTransform>().sizeDelta.x, 0);
        nextPageStartingPosLeft = new Vector2(-GetComponent<RectTransform>().sizeDelta.x, 0);

        if (pages.Length == 0 || pages.Length == 1) {
            Debug.LogWarning("No pages added");
            arrowLeft.SetActive(false);
            arrowRight.SetActive(false);
        }
    }

    public void SwitchRight() {
        currentPage++;
        if (currentPage >= pages.Length) currentPage = 0;
        pages[currentPage].GetComponent<RectTransform>().anchoredPosition = nextPageStartingPosRight;
        StartCoroutine(SwapPageRight());
        pages[currentPage].SetActive(true);
    }       
    
    public void SwitchLeft() {
        //pages[currentPage].SetActive(false);
        currentPage--;
        if (currentPage < 0) currentPage = pages.Length-1;
        pages[currentPage].GetComponent<RectTransform>().anchoredPosition = nextPageStartingPosLeft;
        StartCoroutine(SwapPageLeft());
        pages[currentPage].SetActive(true);
    }

    IEnumerator SwapPageRight() {
        while (pages[currentPage].GetComponent<RectTransform>().anchoredPosition.x >= 0) {
            yield return new WaitForSeconds(0.01f);
            foreach (GameObject page in pages) {
                page.transform.position -= new Vector3(10,0,0);
            }
        }

        pages[currentPage].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    IEnumerator SwapPageLeft() {
        while (pages[currentPage].GetComponent<RectTransform>().anchoredPosition.x <= 0) {
            yield return new WaitForSeconds(0.01f);
            foreach (GameObject page in pages) {
                page.transform.position += new Vector3(10, 0, 0);
            }
        }

        pages[currentPage].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}

