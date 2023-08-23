using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HandPresence : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody rb;
    private Collider[] colliders;
    private float colsDelay = 0.5f;
    [SerializeField] private InputActionReference turnReference;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        turnReference.action.started += OnRotate;
    }
    private void OnDestroy()
    {
        turnReference.action.started -= OnRotate;
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        rb.velocity = (target.position - transform.position) / delta;

        Quaternion rotationDiff = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiff.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDiffInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDiffInDegree * Mathf.Deg2Rad / delta);
    }
    private void OnRotate(InputAction.CallbackContext context)
    {
        rb.transform.position = target.position;
        rb.transform.rotation = target.rotation;
    }

    public void SwitchHandCollider(bool newState)
    {
        StartCoroutine(SwitchColliders(newState, (newState? colsDelay : 0)));
    }

    private IEnumerator SwitchColliders(bool newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (Collider col in colliders)
        {
            col.gameObject.layer = LayerMask.NameToLayer(newState ? "PlayerHands" : "IgnoreTools");
            Debug.Log(col.gameObject.layer);
        }
    }
}
