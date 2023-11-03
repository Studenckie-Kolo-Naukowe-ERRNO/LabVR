using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cpu Architecture", menuName = "ScriptableObjects/New Cpu Architecture")]
public class CpuArchitecture : ScriptableObject {

    public string cpuBrand;
    public string cpuArchitectureName;
    public int ipc;
    public float multiThreadMultipiler;
}
