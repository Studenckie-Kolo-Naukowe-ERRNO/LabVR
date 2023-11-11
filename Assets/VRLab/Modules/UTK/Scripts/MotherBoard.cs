using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace UTKLab
{
    public class MotherBoard : MonoBehaviour
    {
        [Header("Connected components")]
        [SerializeField] private Cpu CPU;
        [SerializeField] private List<HardDrive> drives;
        public void AddCpu(SelectEnterEventArgs args)
        {
            Debug.Log(args.interactableObject.transform.name);
            
            if (args.interactableObject.transform.TryGetComponent(out Cpu c))
                CPU = c;
        }

        public void RemoveCpu(SelectExitEventArgs args)
        {
            if (args.interactorObject.transform.gameObject.TryGetComponent(out Cpu c))
                CPU = null;
        }

        public void AddDrive(SelectEnterEventArgs args)
        {
            if (args.interactorObject.transform.gameObject.TryGetComponent(out HardDrive hd))
                drives.Add(hd);
        }

        public void RemoveDrive(SelectExitEventArgs args)
        {
            if (args.interactorObject.transform.gameObject.TryGetComponent(out HardDrive hd))
                drives.Remove(hd);
        }
    }
}