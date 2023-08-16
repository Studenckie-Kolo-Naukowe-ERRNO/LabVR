using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorVisualizer : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject vectorXvis;
    [SerializeField] private GameObject vectorYvis;
    [SerializeField] private GameObject vectorZvis;
    [SerializeField] private float multiplier = 1f;
    void Start()
    {
        if(rb == null) rb = GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {
        vectorXvis.transform.localScale = new Vector3(rb.velocity.x * multiplier, 1, 1);
        vectorYvis.transform.localScale = new Vector3(1, rb.velocity.y * multiplier, 1);
        vectorZvis.transform.localScale = new Vector3(1, 1, rb.velocity.z * multiplier);
    }
}
