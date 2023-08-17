using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] private Rigidbody ramp;
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
    public void MoveRamp(float angle)
    {
        ramp.MoveRotation(Quaternion.Euler(angle, 0,0));
        if (angleOut != null) angleOut.SetText($"{angle}");
    }
    public void SetStaticRamp(float value)
    {
        rampMaterial.staticFriction = value;
        if(staticRamp != null) staticRamp.SetText($"{value}");
    }
    public void SetStaticBlock(float value)
    {
        blockMaterial.staticFriction = value;
        if (staticBlock != null) staticBlock.SetText($"{value}");
    }
    public void SetDynamicRamp(float value)
    {
        rampMaterial.dynamicFriction = value;
        if (dynamicRamp != null) dynamicRamp.SetText($"{value}");
    }
    public void SetDynamicBlock(float value)
    {
        blockMaterial.dynamicFriction = value;
        if (dynamicBlock != null) dynamicBlock.SetText($"{value}");
    }
}
