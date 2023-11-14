using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UTKLab
{
    public class Cooler : MonoBehaviour
    {
        [Tooltip("TDP in W")]
        [SerializeField] private int TDP;
        public enum type
        {
            Pasywne,
            Aktywne,
            Wodne
        }
        public int Cooling()
        {
            return TDP;
        }
    }
}
