using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSetting : MonoBehaviour {

    private void Start() {
        
    }

    public void OpenTab(GameObject setting) {
        setting.SetActive(true);

        transform.parent.gameObject.SetActive(false);
    }
}
