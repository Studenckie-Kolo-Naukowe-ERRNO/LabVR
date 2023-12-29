using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTKLab {
    public class Cpu : MonoBehaviour, IDevice {

        [Header("Scriptable Objects")]
        [SerializeField] private CpuArchitecture cpuArchitecture;
        [SerializeField] private Connector cpuSocket;

        [Header("CPU specifications")]
        [SerializeField] private bool unlockedMultiplier;
        [SerializeField] private bool integratedGraphics;
        [SerializeField] private string model;
        [SerializeField] private int cores;
        [SerializeField] private int threads;
        [SerializeField] private int TDP;
        [SerializeField] private float clockRate;

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
        public Connector GetCpuSocket()
        {
            return cpuSocket;
        }

        public Connector GetDeviceConnector()
        {
            return cpuSocket;
        }

        public int GetDeviceAdditionalValue()
        {
            return 0;
        }
    }
}
