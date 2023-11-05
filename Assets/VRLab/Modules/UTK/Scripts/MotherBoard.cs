using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace UTKLab
{
    public class MotherBoard : MonoBehaviour
    {
        [SerializeField] private Cpu CPU;
        [SerializeField] private XRSocketInteractor CPUSocketInteractor;
        [SerializeField] private CpuSocket socket;
        private void Start()
        {
        }
        public void SelectEn(SelectEnterEventArgs args)
        {
            //args.interactorObject.
            Debug.Log("SelectEn");
        }
    }
}