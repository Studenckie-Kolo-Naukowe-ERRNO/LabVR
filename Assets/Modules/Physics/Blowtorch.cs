using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blowtorch : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private float maxTemp = 100;
    [SerializeField] private float strength = 1;
    bool currentState = false;
    private List<IFlammable> flammables = new List<IFlammable>();
    public void DoFireStuff(bool state)
    {
        currentState = state;
        if (currentState) fireParticles.Play();
        else fireParticles.Stop();
    }
    [ContextMenu("Toggle")]
    public void ToggleFire()
    {
        DoFireStuff(!currentState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IFlammable flammable))
        {
            flammables.Add(flammable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IFlammable flammable))
        {
            flammables.Remove(flammable);
        }
    }
    private void FixedUpdate()
    {
        if (!currentState) return;

        float heat = strength * Time.fixedDeltaTime;
        for (int i=0; i<flammables.Count;i++)
        {
            flammables[i].HeatUp(heat, maxTemp);
        }
    }
}