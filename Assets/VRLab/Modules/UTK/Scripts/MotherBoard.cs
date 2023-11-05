using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace UTKLab
{
    public class MotherBoard : MonoBehaviour
    {
        [SerializeField] private GameObject CPU;
        [SerializeField] private CpuSocket socket;
        public void AddCpu(SelectEnterEventArgs args)
        {
            CPU = args.interactorObject.transform.gameObject;
        }

        public void RemoveCpu(SelectExitEventArgs args)
        {
            CPU = null;
        }
    }
}