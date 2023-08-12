using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Lightsaber : MonoBehaviour
{
    [SerializeField] private LayerMask sliceableLayer;
    [SerializeField] private Material cutMaterial;
    [SerializeField] private float cutForce = 500;

    [SerializeField] private float igniteTime = 1f;
    private Coroutine seq;
    bool status = false;

    [SerializeField] private Transform startSlicePoint;
    [SerializeField] private Transform endSlicePoint;
    private Vector3 swordLastPos;

    private AudioSource audioSource;
    private void Start()
    {
        status = (transform.localScale.y > 0);
        audioSource = GetComponent<AudioSource>();
    }

    [ContextMenu("Toggle")]
    public void Toggle()
    {
        status = !status;
        Toggle(status);
    }
    public void Toggle(bool activate)
    {
        if (status == activate) return;
        if(seq != null) StopCoroutine(seq);
        if(activate) seq = StartCoroutine(Sequence(0,1));
        else seq = StartCoroutine(Sequence(1,0));

        if(activate) audioSource.Play();
        else audioSource.Stop();
        status = activate;
    }

    public void FixedUpdate() 
    {
        if (status)
        {
            bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
            if (hasHit)
            {
                GameObject targetToSlice = hit.transform.gameObject;
                Vector3 velocity = (endSlicePoint.position - swordLastPos) / Time.fixedDeltaTime;
                Slice(targetToSlice, velocity);
            }
        }
        
        swordLastPos = endSlicePoint.position;
    }

    IEnumerator Sequence(float start, float end)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime/ igniteTime;
            transform.localScale = new Vector3(1, Mathf.Lerp(start, end, time), 1);
            yield return 0;
        }
    }

    public void Slice(GameObject target, Vector3 velocity)
    {
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);

            PreparePiece(upperHull);
            PreparePiece(lowerHull);

            Destroy(target);
        }
    }

    private void PreparePiece(GameObject piece)
    {
        Rigidbody rb = piece.AddComponent<Rigidbody>();
        MeshCollider collider = piece.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.AddExplosionForce(cutForce, piece.transform.position, 1);

        LayerChanger.SetObjectLayer(piece, "Sliceable");
    }
}
