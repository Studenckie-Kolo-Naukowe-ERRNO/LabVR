using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GPU : MonoBehaviour
{
    ///Do obliczen///
    public float TextureMappingUnit;    //Jednostki teksturujace
    public float RasterOperations;  //Jednostki rasteryzujace 
    public float CoreClock; //Taktowanie rdzenia w trybie turbo (ghz)
    ///Benchmark///
    float TFLOPScore = 769.4578387977584f; // wynik benchamarka za 1 TFLOP (Na podstawie 3D Mark - Time Spy (DX12) wynik rtx 3060 (12,7 TFLOP = 9801) )
    ///Wyniki///
    public float PerformanceTFLOPS;
    public float PerformanceSCORE; 
    ///Dodatki///
    public string Interface;    //Interfejs (PCIe)
    public string TGP;  //Zasilanie (W)
    public string MemoryClock;  //Taktowanie pamieci (MHz)
    public string MemoryBandwidth;  //Przepustowosc pamieci (GB/s)
    public string VideoMemory;  //Video memory (GB)
    public string MemoryBus;    //Szyna pamieci (bit)
    public string TypeOfMemory;     //Rodzaj pamieci (GDDR)

    void Start()
    {
        PerformanceTFLOPS = (TextureMappingUnit * RasterOperations * CoreClock) / 1000.0f;
        PerformanceSCORE = (PerformanceTFLOPS * TFLOPScore);
        Mathf.Round(PerformanceSCORE);
        Debug.Log("wydajnoscTFLOPS:"+ PerformanceTFLOPS+"FinalScore:"+ PerformanceSCORE);
    }

}
