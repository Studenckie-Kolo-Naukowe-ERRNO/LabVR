using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ramp : MonoBehaviour
{
    [SerializeField] private Rigidbody ramp;
    [SerializeField] private Transform resetPos;
    [SerializeField] private Rigidbody cube;
    [Header("Materials")]
    [SerializeField] private PhysicMaterial rampMaterial;
    [SerializeField] private PhysicMaterial blockMaterial;
    [Header("Outputs")]
    [SerializeField] private TextMeshProUGUI angleOut;
    [SerializeField] private TextMeshProUGUI staticRamp;
    [SerializeField] private TextMeshProUGUI staticBlock;
    [SerializeField] private TextMeshProUGUI dynamicRamp;
    [SerializeField] private TextMeshProUGUI dynamicBlock;

    [SerializeField] private float speed = 1;
    [SerializeField] private float targetAngle = 0;
    private float currentAngle = 25;
    

    private void Start()
    {
        ramp.rotation = (Quaternion.Euler(currentAngle, ramp.transform.eulerAngles.y, ramp.transform.eulerAngles.z));
        if (angleOut != null) angleOut.SetText($"{ramp.transform.rotation.x}");
        if (staticRamp != null) staticRamp.SetText($"{rampMaterial.staticFriction}");
        if (staticBlock != null) staticBlock.SetText($"{blockMaterial.staticFriction}");
        if (dynamicRamp != null) dynamicRamp.SetText($"{rampMaterial.dynamicFriction}");
        if (dynamicBlock != null) dynamicBlock.SetText($"{blockMaterial.dynamicFriction}");
    }

    public void MoveRamp(float angle)
    {
        targetAngle = angle;
    }
    private void FixedUpdate()
    {
        float diff = Mathf.Abs(currentAngle - targetAngle);
        diff = (float)Math.Round(diff, 2);

        if (diff == 0) return;

        float delta = Time.fixedDeltaTime * speed;

        float step = (delta>=diff)?diff:delta;

        if (currentAngle > targetAngle) step = -step;
        
        currentAngle += step;
        ramp.MoveRotation(Quaternion.Euler(currentAngle, ramp.transform.eulerAngles.y, ramp.transform.eulerAngles.z));
        if (angleOut != null) angleOut.SetText($"{currentAngle.ToString("0.00")}");
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
    [ContextMenu("Reset cube")]
    public void ResetCube()
    {
        cube.transform.position = resetPos.position;
        cube.transform.rotation = resetPos.rotation;
        cube.velocity = Vector3.zero;
    }
}
