using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using VRLabEssentials;

namespace PhysicsLab
{
    public class Lightsaber : MonoBehaviour
    {
        [SerializeField] private LayerMask sliceableLayer;
        [SerializeField] private Material cutMaterial;
        [SerializeField] private float cutForce = 500;
        [SerializeField] private float cutDelay = 0.5f;
        private float cutTime = 0;

        [SerializeField] private float igniteTime = 1f;
        private Coroutine seq;
        private bool status = false;

        [SerializeField] private Transform startSlicePoint;
        [SerializeField] private Transform endSlicePoint;
        private Vector3 swordLastPos;

        private AudioSource audioSource;
        [SerializeField] private AudioClip[] cutSounds;
        [SerializeField] private AudioClip[] swingSounds;
        [SerializeField] private float swingDistance;
        private float swingTime;
        
        private void Start()
        {
            status = (transform.localScale.y > 0.1f);
            audioSource = GetComponent<AudioSource>();
        }

        [ContextMenu("Toggle")]
        public void Toggle()
        {
            Toggle(!status);
        }

        public void Toggle(bool activate)
        {
            if (status == activate) return;
            if (seq != null) StopCoroutine(seq);
            if (activate) seq = StartCoroutine(Sequence(0, 1));
            else seq = StartCoroutine(Sequence(1, 0));

            if (activate) audioSource.Play();
            else audioSource.Stop();
            status = activate;
        }

        public void FixedUpdate()
        {
            if (status)
            {
                if (Time.time >= cutTime)
                {
                    bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
                    if (hasHit)
                    {

                        GameObject objectToDestory = null;
            GameObject targetToSlice = hit.transform.gameObject;
                        targetToSlice.TryGetComponent(out Tool t);
                        if (t != null && t.CanBeSliced())
                        {

                            objectToDestory = targetToSlice;

                            targetToSlice = t.GetThisObjectMesh();
                        }
                        if ((t != null && t.CanBeSliced()) || t == null)
                        {
                            cutTime = Time.time + cutDelay;
                            Vector3 velocity = (endSlicePoint.position - swordLastPos) / Time.fixedDeltaTime;
                            Slice(targetToSlice, velocity);
                        }
                        
                        if (objectToDestory != null)
                        {
                            Destroy(objectToDestory);
                        }
                    }
                }
                if(Time.time >= swingTime && Vector3.Distance(swordLastPos, endSlicePoint.position) > swingDistance)
                {
                    swingTime = Time.time + 1;
                    audioSource.PlayOneShot(swingSounds[Random.Range(0, swingSounds.Length)]);
                    Debug.Log("Swing");
                }
            }

            swordLastPos = endSlicePoint.position;
        }

        IEnumerator Sequence(float start, float end)
        {
            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime / igniteTime;
                transform.localScale = new Vector3(1, Mathf.Lerp(start, end, time), 1);
                yield return null;
            }
        }

        public void Slice(GameObject target, Vector3 velocity)
        {
            Vector3 pos = target.transform.position;
            Vector3 rot = target.transform.rotation.eulerAngles;
            Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
            planeNormal.Normalize();

            SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

            if (hull != null)
            {
                GameObject upperHull = hull.CreateUpperHull(target, cutMaterial);
                GameObject lowerHull = hull.CreateLowerHull(target, cutMaterial);

                PreparePiece(upperHull, pos, rot);
                PreparePiece(lowerHull, pos, rot);

                Destroy(target);

                audioSource.PlayOneShot(cutSounds[Random.Range(0,cutSounds.Length)]);
            }
        }

        private void PreparePiece(GameObject piece, Vector3 position, Vector3 rotation)
        {
            Rigidbody rb = piece.AddComponent<Rigidbody>();
            MeshCollider collider = piece.AddComponent<MeshCollider>();
            collider.convex = true;

            piece.transform.position = position;
            piece.transform.rotation = Quaternion.Euler(rotation);

            rb.AddExplosionForce(cutForce, piece.transform.position, 1);

            piece.layer = LayerMask.NameToLayer("Sliceable");
        }
    }
}