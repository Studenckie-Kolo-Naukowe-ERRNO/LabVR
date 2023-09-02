using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PhysicsLab
{
    public interface IFlammable
    {
        public void HeatUp(float amout, float maxTemp);
    }
}