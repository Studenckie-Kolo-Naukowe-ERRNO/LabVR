using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicCollisionsController : MonoBehaviour
{
    [SerializeField] private float releaseStrength = 10.0f;
    [SerializeField] private Rigidbody[] rb = new Rigidbody[2];
    [SerializeField] private Transform[] points = new Transform[2];
    void Start()
    {
        ResetExperiment();
    }

    [ContextMenu("ResetExperiment")]
    public void ResetExperiment()
    {
        for(int i = 0; i < rb.Length && i < points.Length; i++)
        {
            rb[i].velocity = Vector3.zero;
            rb[i].transform.position = points[i].position;
        }
        
    }
    [ContextMenu("StartExperiment")]
    public void StartExperiment()
    {
        rb[0].velocity = rb[0].transform.forward * releaseStrength;
        rb[1].velocity = rb[1].transform.forward * releaseStrength;
    }
}
