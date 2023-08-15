using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresence : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody rb;
    private Collider[] colliders;
    private float colsDelay = 0.5f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    private void LateUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.deltaTime;

        Quaternion rotationDiff = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiff.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDiffInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDiffInDegree * Mathf.Deg2Rad / Time.deltaTime);
    }

    public void SwitchHandCollider(bool newState)
    {
        if (newState) StartCoroutine(SwitchColliders(newState,colsDelay));
        else StartCoroutine(SwitchColliders(newState, 0));
    }

    private IEnumerator SwitchColliders(bool newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (Collider col in colliders)
        {
            //col.enabled = newState;
            col.gameObject.layer = LayerMask.NameToLayer(newState ? "PlayerHands" : "IgnoreTools");
        }
    }
}
