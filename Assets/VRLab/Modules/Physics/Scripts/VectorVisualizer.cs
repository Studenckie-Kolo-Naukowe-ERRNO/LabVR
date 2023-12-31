using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PhysicsLab
{
    public class VectorVisualizer : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject vectorXvis;
        [SerializeField] private GameObject vectorYvis;
        [SerializeField] private GameObject vectorZvis;
        [SerializeField] private float multiplier = 1f;

        [SerializeField]
        private TextMeshProUGUI textOutput;

        void Start()
        {
            if (rb == null) rb = GetComponentInParent<Rigidbody>();
            if (textOutput == null) textOutput = GetComponentInParent<TextMeshProUGUI>();
        }

        void FixedUpdate()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            vectorXvis.transform.localScale = new Vector3(rb.velocity.x * multiplier, 1, 1);
            vectorYvis.transform.localScale = new Vector3(1, rb.velocity.y * multiplier, 1);
            vectorZvis.transform.localScale = new Vector3(1, 1, rb.velocity.z * multiplier);

            if (textOutput != null)
            {
                textOutput.SetText($"{rb.velocity.magnitude.ToString("0.00")}");
            }
        }
    }
}