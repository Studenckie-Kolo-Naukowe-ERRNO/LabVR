using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTKLab;

public class GPU : MonoBehaviour, IDevice
{
    [SerializeField] private GPUData data;

    public GPUData GetGPUData() 
    { 
        return data; 
    }

    public int GetDeviceAdditionalValue()
    {
        return 0;
    }

    public Connector GetDeviceConnector()
    {
        return data.GetCardConnector();
    }
}
