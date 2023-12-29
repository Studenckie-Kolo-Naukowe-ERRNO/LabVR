using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace UTKLab
{
    public class VRLabSocketInteractor : XRSocketInteractor
    {
        [Header("Filter PARAMS")]
        [SerializeField] private Connector socket;


        public override bool CanHover(IXRHoverInteractable interactable)
        {
            if (interactable is MonoBehaviour gameObject && interactable.transform.TryGetComponent<IDevice>(out IDevice c))
            {
               return (c.GetDeviceConnector() == socket && base.CanHover(interactable));
            }
            return base.CanHover(interactable);
        }

        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            if (interactable is MonoBehaviour gameObject && interactable.transform.TryGetComponent<IDevice>(out IDevice c))
            {
                return (c.GetDeviceConnector() == socket && base.CanSelect(interactable));
            }
            return base.CanSelect(interactable);
        }
    }
}