using UnityEngine;

public class FirstOnLayer : MonoBehaviour {

    private void Start() {
        transform.SetAsLastSibling();
        gameObject.SetActive(false);
    }
}
