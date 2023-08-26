using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] private Rigidbody ramp;
    [SerializeField] private float debugRampRotation;
    [Header("Materials")]
    [SerializeField] private PhysicMaterial rampMaterial;
    [SerializeField] private PhysicMaterial blockMaterial;
    [Header("Outputs")]
    [SerializeField] private TextMeshProUGUI angleOut;
    [SerializeField] private TextMeshProUGUI staticRamp;
    [SerializeField] private TextMeshProUGUI staticBlock;
    [SerializeField] private TextMeshProUGUI dynamicRamp;
    [SerializeField] private TextMeshProUGUI dynamicBlock;
    

    private void Start()
    {
        if (angleOut != null) angleOut.SetText($"{ramp.transform.rotation.x}");
        if (staticRamp != null) staticRamp.SetText($"{rampMaterial.staticFriction}");
        if (staticBlock != null) staticBlock.SetText($"{blockMaterial.staticFriction}");
        if (dynamicRamp != null) dynamicRamp.SetText($"{rampMaterial.dynamicFriction}");
        if (dynamicBlock != null) dynamicBlock.SetText($"{blockMaterial.dynamicFriction}");
    }
    [ContextMenu("UpdateRampAxis")]
    public void UpdateRampAxis()
    {
        MoveRamp(debugRampRotation);
    }
    public void MoveRamp(float angle)
    {
        ramp.MoveRotation(Quaternion.Euler(angle, ramp.transform.eulerAngles.y, ramp.transform.eulerAngles.z));
        if (angleOut != null) angleOut.SetText($"{angle}");
    }
    public void SetStaticRamp(float value)
    {
        value = (float)Math.Round(value,2);
        rampMaterial.staticFriction = value;
        if(staticRamp != null) staticRamp.SetText($"{value}");
    }
    public void SetStaticBlock(float value)
    {
        value = (float)Math.Round(value, 2);
        blockMaterial.staticFriction = value;
        if (staticBlock != null) staticBlock.SetText($"{value}");
    }
    public void SetDynamicRamp(float value)
    {
        value = (float)Math.Round(value, 2);
        rampMaterial.dynamicFriction = value;
        if (dynamicRamp != null) dynamicRamp.SetText($"{value}");
    }
    public void SetDynamicBlock(float value)
    {
        value = (float)Math.Round(value, 2);
        blockMaterial.dynamicFriction = value;
        if (dynamicBlock != null) dynamicBlock.SetText($"{value}");
    }
}
