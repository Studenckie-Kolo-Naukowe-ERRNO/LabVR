using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour {

    private void OnTransformParentChanged() {
        Destroy(gameObject, 4);
    }
}
