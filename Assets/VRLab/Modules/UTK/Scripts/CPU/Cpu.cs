using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cpu : MonoBehaviour { 

    [SerializeField] private CpuArchitecture cpuArchitecture;
    [SerializeField] private CpuSocket cpuSocket;
    [SerializeField] private string model;
    [SerializeField] private float clockRate;
    [SerializeField] private int cores;
    [SerializeField] private int threads;
    [SerializeField] private bool unlockedMultiplier;
    [SerializeField] private bool integratedGraphics;
    [SerializeField] private int TDP;

    [ContextMenu("PrintPerformance")]
    public void PrintPerformance() {
        Debug.Log(SingleThreadPerformance());
        Debug.Log(MultiThreadPerformance());
    }

    private float SingleThreadPerformance() {
        return clockRate * cpuArchitecture.ipc;
    }

    private float MultiThreadPerformance() {
        return clockRate * cpuArchitecture.ipc * threads * cpuArchitecture.multiThreadMultipiler;
    }

}
