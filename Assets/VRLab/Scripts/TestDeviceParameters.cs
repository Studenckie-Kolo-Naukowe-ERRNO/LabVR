using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestDeviceParameters : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    void Start()
    {
        outputText.text = "";
        outputText.text += $"{SystemInfo.deviceName}\n";
        outputText.text += $"{SystemInfo.deviceModel}\n";
        outputText.text += $"{SystemInfo.deviceType}\n";
        float[] rates;
        Unity.XR.Oculus.Performance.TryGetAvailableDisplayRefreshRates(out rates);
        foreach(float f in rates)
        {
            outputText.text += $"{f}Hz\n";
        }
        Unity.XR.Oculus.Performance.TrySetDisplayRefreshRate(90);
        float rate;
        Unity.XR.Oculus.Performance.TryGetDisplayRefreshRate(out rate);
        outputText.text += $"current: {rate}Hz\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
