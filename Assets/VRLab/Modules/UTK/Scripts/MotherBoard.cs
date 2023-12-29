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
        [SerializeField] private List<GPU> graphicCards;
        [SerializeField] private List<RAMStick> memoryModules;
        private bool isPowered = false;

        [ContextMenu("Power motherboard on")]
        public void PowerMotherboardOn()
        {
            PowerMotherboard(true);
        }
        
        [ContextMenu("Power motherboard off")]
        public void PowerMotherboardOff()
        {
            PowerMotherboard(false);
        }

        public void PowerMotherboard(bool newState)
        {
            if (isPowered == newState) return;

            if(newState)
            {
                Debug.Log($"Powering is {POST()}");
            }
            else
            {
                Debug.Log($"Shut down");
            }

            isPowered = newState;
        }
        public bool POST()
        {
            if (CPU == null)
            {
                Debug.Log("No CPU!");
                return false;
            }
            if (memoryModules.Count == 0)
            {
                Debug.Log("No memory!");
                return false;
            }
            if (graphicCards.Count == 0)
            {
                Debug.Log("No GPU!");
                return false;
            }

            return true;
        }
        public bool IsPoweredOn()
        {
            return isPowered;
        }

        public void AddCpu(SelectEnterEventArgs args)
        {
            if (args.interactableObject.transform.TryGetComponent(out Cpu c)) 
            {
                PowerMotherboard(false);
                CPU = c;
            }

                
        }

        public void RemoveCpu(SelectExitEventArgs args)
        {
            if (args.interactableObject.transform.gameObject.TryGetComponent(out Cpu c))
            {
                PowerMotherboard(false);
                CPU = null;
            }
        }

        public void AddDrive(SelectEnterEventArgs args)
        {
            if (args.interactableObject.transform.gameObject.TryGetComponent(out HardDrive hd))
            {
                PowerMotherboard(false);
                drives.Add(hd);
            }
        }

        public void RemoveDrive(SelectExitEventArgs args)
        {
            if (args.interactableObject.transform.gameObject.TryGetComponent(out HardDrive hd))
            {
                PowerMotherboard(false);
                drives.Remove(hd);
            }
        }

        public void AddPCIeDevice(SelectEnterEventArgs args)
        {
            if (args.interactableObject.transform.gameObject.TryGetComponent(out GPU gpu))
            {
                PowerMotherboard(false);
                graphicCards.Add(gpu);
            }
        }

        public void RemovePCIeDevice(SelectExitEventArgs args)
        {
            if (args.interactableObject.transform.gameObject.TryGetComponent(out GPU gpu))
            {
                PowerMotherboard(false);
                graphicCards.Remove(gpu);
            }
        }

        public void AddRAMModule(SelectEnterEventArgs args)
        {
            if (args.interactableObject.transform.gameObject.TryGetComponent(out RAMStick module))
            {
                PowerMotherboard(false);
                memoryModules.Add(module);
            }
        }

        public void RemoveRAMModule(SelectExitEventArgs args)
        {
            if (args.interactableObject.transform.gameObject.TryGetComponent(out RAMStick module))
            {
                PowerMotherboard(false);
                memoryModules.Remove(module);
            }
        }
   
    }
}