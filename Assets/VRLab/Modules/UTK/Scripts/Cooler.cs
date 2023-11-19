using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UTKLab
{
    public class Cooler : MonoBehaviour
    {
        [Tooltip("TDP in W")]
        [SerializeField] private int coolingCapacity;
        [Tooltip("Power usage")]
        [SerializeField] private int powerUsage;
        public enum type
        {
            Pasywne,
            Aktywne,
            Wodne
        }

        public int GetCoolingCapacity()
        {
            return coolingCapacity;
        }
        public int GetPowerUsage()
        {
            return powerUsage;
        }
    }
}
