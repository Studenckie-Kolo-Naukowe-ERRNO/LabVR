using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTKLab
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Components/GPU Arch")]
    public class GPUArchitecture : ScriptableObject
    {
        [SerializeField] private string architectureName = "Pascal <3";
        [SerializeField] private float TFLOPScore = 769.4f; // wynik benchamarka za 1 TFLOP (Na podstawie 3D Mark - Time Spy (DX12) wynik rtx 3060 (12,7 TFLOP = 9801) )
    
        public string GetArchName()
        {
            return architectureName;
        }
        public float GetTFLOPScore()
        {
            return TFLOPScore;
        }
    }
}
