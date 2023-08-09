using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class ButtonVR : MonoBehaviour
{
    [SerializeField] private GameObject buttonBody;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float treshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    [SerializeField] private float delay = 0.5f;
    [Header("Events")]
    [SerializeField] private UnityEvent onPressed;
    [SerializeField] private UnityEvent onReleased;
    [Header("Toggle")]
    [SerializeField] private UnityEvent onToggleA;
    [SerializeField] private UnityEvent onToggleB;

    private bool pressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    private bool toogled = true;
    private AudioSource audioSource;
    private float pressTime = 0;

    void Start()
    {
        startPos = buttonBody.transform.localPosition;
        joint = buttonBody.GetComponent<ConfigurableJoint>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time < pressTime) return;
        if (!pressed && GetValue() + treshold >=1)
        {
            Pressed();
        }

        if (pressed && GetValue() - treshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        float  value = Vector3.Distance(startPos, buttonBody.transform.localPosition) / joint.linearLimit.limit;
        if (Math.Abs(value) < deadZone) value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        OnClick();
        pressed = true;
        onPressed.Invoke();

        if(toogled)onToggleA.Invoke();
        else onToggleB.Invoke();

        toogled = !toogled;
    }
    private void Released()
    {
        OnClick();
        pressed = false;
        onReleased.Invoke();
    }

    private void OnClick()
    {
        audioSource.PlayOneShot(clickSound);
        pressTime = Time.time + delay;
    }
}
