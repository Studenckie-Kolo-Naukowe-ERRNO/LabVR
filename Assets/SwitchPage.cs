using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPage : MonoBehaviour {
    [SerializeField] private GameObject[] pages;
    private int currentPage = 0;

    public void SwitchRight() {
        pages[currentPage].SetActive(false);
        currentPage++;
        if (currentPage >= pages.Length) currentPage = 0;
        pages[currentPage].SetActive(true);
    }       
    
    public void SwitchLeft() {
        pages[currentPage].SetActive(false);
        currentPage--;
        pages[currentPage].SetActive(true);
    }
}
