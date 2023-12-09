using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstOnLayer : MonoBehaviour {

    void Start() {
        transform.SetAsLastSibling();
    }
}
