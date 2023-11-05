using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTKLab {
    [CreateAssetMenu(fileName = "New Cpu Architecture", menuName = "ScriptableObjects/Components/Cpu Architecture")]
    public class CpuArchitecture : ScriptableObject {

        public string cpuBrand;
        public string cpuArchitectureName;
        public int ipc;
        public float multiThreadMultipiler;
    }
}
