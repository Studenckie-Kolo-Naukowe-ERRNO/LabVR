using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
namespace VRLabEssentials
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class Tool : XRGrabInteractable, IItem
    {
        [Header("Item")]
        [SerializeField] private ItemData data;
        [SerializeField] private bool canBeSliced;
        [SerializeField] private GameObject thisObiectMesh;
        [Header("Effects")]
        [SerializeField] protected AudioClip pickSound;
        [SerializeField] protected AudioClip useSound;
        private AudioSource itemAudioSource;
        [Header("Events")]
        [SerializeField] protected UnityEvent OnToggle;
        [SerializeField] protected UnityEvent OnUnToggle;
        [SerializeField] protected UnityEvent InHand;
        
        private Rigidbody rb;

        protected bool toggled = false;
        protected byte inHands = 0;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            itemAudioSource = GetComponent<AudioSource>();
        }
        private void Update()
        {
            if (IsHolded())
            {
                InHand.Invoke();
            }
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            if(inHands == 0 && pickSound != null)
            {
                itemAudioSource.PlayOneShot(pickSound);
            }
            inHands++;

            UpdateMassCenter();

            base.OnSelectEntered(args);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            inHands--;

            UpdateMassCenter();

            base.OnSelectExited(args);
        }

        protected override void OnActivated(ActivateEventArgs args)
        {
            if (toggled) OnUnToggle.Invoke();
            else OnToggle.Invoke();
            toggled = !toggled;

            if(useSound != null) itemAudioSource.PlayOneShot(useSound);
            base.OnActivated(args);
        }
        public ItemData GetItemData()
        {
            return data;
        }

        public GameObject ThisObject()
        {
            return this.gameObject;
        }
        public bool CanBeSliced()
        {
            return canBeSliced && !IsHolded();
        }
        public bool IsHolded()
        {
            return inHands > 0;
        }

        public void SetParentToXRRig()
        {
            transform.SetParent(this.GetOldestInteractorSelecting().transform);
        }

        public void SetParentToWorld()
        {
            transform.SetParent(null);
        }

        private void UpdateMassCenter()
        {
            if (rb == null) return;
            if (IsHolded())
            {
                SetParentToXRRig();
                rb.centerOfMass = Vector3.zero;
            }
            else
            {
                SetParentToWorld();
                rb.ResetCenterOfMass();
            }

        }

        public GameObject GetThisObjectMesh()
        {
            if (thisObiectMesh == null) return ThisObject();
            else return thisObiectMesh;
        }

        public void SetThisObjectMesh(GameObject newMesh)
        {
            thisObiectMesh = newMesh;
        }
    }
}