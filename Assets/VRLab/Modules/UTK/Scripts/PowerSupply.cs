using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTKLab
{
    public class PowerSupply : MonoBehaviour
    {
        [Tooltip("Power in W")]
        [SerializeField] private int power;
        public enum Certificate
        {
            Bronze = 82,
            Silver = 85,
            Gold = 87,
            Platinum = 89,
            Titanium = 92
        }
        [Tooltip("Energetic Certificate")]
        [SerializeField] private Certificate certificate;

        public int OutputPower()
        {
            return (int)(power * ((int)certificate / 100));
        }
    }
}
