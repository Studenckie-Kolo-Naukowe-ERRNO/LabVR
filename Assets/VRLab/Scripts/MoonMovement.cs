using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMovement : MonoBehaviour {
    [SerializeField] private GameObject earthGameObject;
    [SerializeField] private float orbitSpeed;
    [SerializeField] private float orbitRadius;

    private void Start() {
        Vector3 earthPosition = earthGameObject.transform.position;
        Vector3 initialPosition = earthPosition + new Vector3(orbitRadius, 0f, 0f);

        transform.position = initialPosition;
    }

    private void Update() {
        Vector3 earthPosition = earthGameObject.transform.position;
        float angle = orbitSpeed * Time.deltaTime;

        transform.RotateAround(earthPosition, Vector3.up, angle);
    }
}
