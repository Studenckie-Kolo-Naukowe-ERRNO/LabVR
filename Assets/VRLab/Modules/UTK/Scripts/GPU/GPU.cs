using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTKLab;

public class GPU : MonoBehaviour
{
    [SerializeField] private GPUData data;
    public GPUData GetGPUData() 
    { 
        return data; 
    }
}
