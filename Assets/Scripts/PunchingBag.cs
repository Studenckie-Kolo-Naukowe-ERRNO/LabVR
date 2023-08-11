using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PunchingBag : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI output;
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
        output.SetText(collisionForce.magnitude.ToString());
    }
}
