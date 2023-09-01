using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace VRLabEssentials
{
    public class PunchingBag : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI output;
        private void OnCollisionEnter(Collision collision)
        {
            Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
            if (collisionForce.magnitude > 0) output.SetText(collisionForce.magnitude.ToString());
        }
    }
}