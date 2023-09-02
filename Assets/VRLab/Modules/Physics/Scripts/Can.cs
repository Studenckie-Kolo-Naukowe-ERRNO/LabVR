using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRLabEssentials;

namespace PhysicsLab
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Tool))]
    public class Can : MonoBehaviour, IFlammable
    {
        [SerializeField] private float temperature = 0;
        [SerializeField] private GameObject[] cans;
        [SerializeField] private Gradient heatGradient;
        [SerializeField] private bool bursted = false;
        [SerializeField] private float burstStrength = 2f;
        [SerializeField] private float burstTime = 2f;
        [SerializeField] private ParticleSystem burstParticles;
        private Material material;
        private Rigidbody rb;
        private Tool thisTool;
        void Start()
        {
            thisTool = GetComponent<Tool>();
            rb = GetComponent<Rigidbody>();
            material = cans[0].GetComponent<Renderer>().material;
            cans[0].SetActive(true);
            cans[1].SetActive(false);
            thisTool.SetThisObjectMesh(cans[0]);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (temperature > 0)
            {
                HeatUp(-Time.fixedDeltaTime * 4, 100);
            }
        }
        public void HeatUp(float amout, float maxTemp)
        {
            if (temperature > 70 && amout < -30)
            {
                CrushCan();
                return;
            }
            if (maxTemp > temperature) temperature += amout;
            if (material) material.color = heatGradient.Evaluate(temperature / 100);

        }
        private void CrushCan()
        {
            if(cans[0])cans[0].SetActive(false);
            if(cans[1])cans[1].SetActive(true);
            material = cans[1].GetComponent<Renderer>().material;
            bursted = true;
            thisTool.SetThisObjectMesh(cans[1]);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (bursted) return;

            Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
            if (collisionForce.magnitude > 400) Burst();
        }
        [ContextMenu("Burst can")]
        public void Burst()
        {
            bursted = true;
            StopAllCoroutines();
            StartCoroutine(BurstAction());
        }
        private IEnumerator BurstAction()
        {
            burstParticles.Stop();
            ParticleSystem.MainModule main = burstParticles.main;
            main.duration = burstTime;
            burstParticles.Play();
            float timer = 0;
            rb.AddForce(rb.transform.up * burstStrength, ForceMode.Impulse);
            while (timer <= burstTime)
            {
                timer += Time.fixedDeltaTime;
                rb.AddForce(rb.transform.up * burstStrength, ForceMode.Force);
                yield return new WaitForFixedUpdate();
            }
            rb.AddForce(rb.transform.up * burstStrength * 2, ForceMode.Impulse);
            CrushCan();
        }
    }
}