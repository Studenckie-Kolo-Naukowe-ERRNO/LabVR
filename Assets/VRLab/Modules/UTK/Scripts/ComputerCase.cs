using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace UTKLab
{
    public class ComputerCase : MonoBehaviour
    {
        [SerializeField] private MotherBoard mb;
        private float buttonClickTime = 0;
        private float buttonTimeLimit = 1.0f;
        public void AddMotherboard(SelectEnterEventArgs args)
        {
            if (args.interactableObject.transform.TryGetComponent(out MotherBoard m))
            {
                mb.PowerMotherboard(false);
                mb = m;
            }
        }

        public void RemoveMotherboard(SelectExitEventArgs args)
        {
            if (args.interactableObject.transform.TryGetComponent(out MotherBoard m))
            {
                mb.PowerMotherboard(false);
                mb = null;
            }
        }

        public void PowerButtonAction()
        {
            Debug.Log("Clicked power button");
            if (mb != null && Time.time >= buttonClickTime)
            {
                mb.PowerMotherboard(!mb.IsPoweredOn());
                buttonClickTime = Time.time + buttonTimeLimit; 
            }
        }
    }
}