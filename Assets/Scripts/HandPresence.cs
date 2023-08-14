using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresence : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;
        Debug.Log(rb.velocity);

        Quaternion rotationDiff = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiff.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDiffInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDiffInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
