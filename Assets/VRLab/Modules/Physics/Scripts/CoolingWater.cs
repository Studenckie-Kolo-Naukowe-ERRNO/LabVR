using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PhysicsLab
{
    public class CoolingWater : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IFlammable flammable))
            {
                flammable.HeatUp(-35, 100);
            }
        }
    }
}