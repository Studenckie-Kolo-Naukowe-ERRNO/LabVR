using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    [SerializeField] private InputActionProperty pinchAnimAction;
    [SerializeField] private InputActionProperty gripAnimAction;
    private Animator handAnimator;
    void Start()
    {
        handAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float triggerValue = pinchAnimAction.action.ReadValue<float>();
        float gripValue = gripAnimAction.action.ReadValue<float>();

        handAnimator.SetFloat("Trigger",triggerValue);
        handAnimator.SetFloat("Grip", gripValue);
    }
}
