using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTKLab 
{
    [CreateAssetMenu(menuName = "Components/GPU")]
    public class GPUData : ScriptableObject
    {
        ///Do obliczen///
        [SerializeField] private float textureMappingUnit;    //Jednostki teksturujace
        [SerializeField] private float rasterUnits;  //Jednostki rasteryzujace
        [SerializeField] private float coreClock; //Taktowanie rdzenia w trybie turbo (ghz)
        ///Benchmark///
        [SerializeField] private GPUArchitecture architecture;
        ///Dodatki///
        [SerializeField] private uint GenPCIE;
        [SerializeField] private uint PCIELanes;
        [SerializeField] private uint memoryClock;  //Taktowanie pamieci (MHz)
        [SerializeField] private float memoryBandwidth;  //Przepustowosc pamieci (GB/s)
        [SerializeField] private uint videoMemory;  //Video memory (MB)
        [SerializeField] private uint memoryBus;    //Szyna pamieci (bit)
        public enum memoryType
        {
            GDDR,
            HMB
        }
        [SerializeField] private memoryType typeOfMemory;     //Rodzaj pamieci (GDDR)
        [SerializeField] private string genOfMemory;

        public float PerformanceTFLOPS()
        {
            return (textureMappingUnit * rasterUnits * coreClock) / 1000.0f;
        }

        public int PerformanceSCORE() 
        {
            return (int)(PerformanceTFLOPS() * architecture.GetTFLOPScore());
        }
    }
}

