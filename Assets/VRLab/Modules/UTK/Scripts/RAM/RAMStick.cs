using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTKLab
{
    public class RAMStick : MonoBehaviour, IDevice
    {
        //zmienne obliczeniowe
        [Tooltip("Frequency in MHz")]
        [SerializeField] private int frequency; //Czestotliwosc taktowania
        [Tooltip("Capacity in MB")]
        [SerializeField] private int capacity; //Pojemnosc 
        [Tooltip("Latency CL")]
        [SerializeField] private int latency; //opoznienie ta liczba przy CL
        [Tooltip("Caacity in V")]
        [SerializeField] private float voltage; //napiecie
        public enum type
        {
            DDR1 = 1,
            DDR2 = 2,
            DDR3 = 4,
            DDR4 = 8,
            DDR5 = 16
        }
        [SerializeField] private type RAMType;
        [SerializeField] private Connector memorySocket;
        //oblicznia
        public int PerformanceScore()
        {
            return (int)((frequency * frequency * capacity * (int)RAMType) / (200000 * latency));
        }

        public Connector GetDeviceConnector()
        {
            return memorySocket;
        }

        public int GetDeviceAdditionalValue()
        {
            return 0;
        }
    }
}
