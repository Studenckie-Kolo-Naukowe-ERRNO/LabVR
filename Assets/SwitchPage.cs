using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchPage : MonoBehaviour {
    [SerializeField] private GameObject[] pages;
    private int currentPage = 0;
    private Vector2 nextPageStartingPos;

    private void Start() {
        nextPageStartingPos = new Vector2(GetComponent<RectTransform>().sizeDelta.x, 0);
    }

    public void SwitchRight() {
        currentPage++;
        if (currentPage >= pages.Length) currentPage = 0;
        SwapAnimation();
        pages[currentPage].SetActive(true);
    }       
    
    public void SwitchLeft() {
        pages[currentPage].SetActive(false);
        currentPage--;
        pages[currentPage].SetActive(true);
    }

    private void SwapAnimation() {
        pages[currentPage].GetComponent<RectTransform>().anchoredPosition = nextPageStartingPos;
        StartCoroutine(SwapPage());
    }

    IEnumerator SwapPage() {
        while (pages[currentPage].GetComponent<RectTransform>().anchoredPosition.x >= 0) {
            yield return new WaitForSeconds(0.01f);
            foreach (GameObject page in pages) {
                page.transform.position -= new Vector3(10,0,0);
            }
        }

        pages[currentPage].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
